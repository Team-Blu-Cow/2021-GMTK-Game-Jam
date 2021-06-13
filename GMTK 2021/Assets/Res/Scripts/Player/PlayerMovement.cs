using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField]
    private int m_maxSpeed;

    [SerializeField]
    [Range(0, 100)]
    private int m_jumpHeight;

    [Header("Sensors")]
    [SerializeField]
    private Transform m_groundSensor;

    [SerializeField]
    private float m_groundSensorRadius;

    private LayerMask walkable;
    private LayerMask pickupable;

    [SerializeField]
    private Transform m_pickupSensor;

    [SerializeField]
    private float m_pickupSensorRadius;

    [SerializeField]
    private float m_wallDetectionDist;

    [Header("Transforms")]
    [SerializeField]
    private Transform m_cableHoldPoint;

    [SerializeField]
    private Transform m_HoldPoint;

    private bool m_grounded = false;
    private bool m_pickup;
    private GameObject m_pickedUp;

    private Vector2 m_velocity;
    private float x_dir;
    private Rigidbody2D m_rb;

    private float m_pickupRange = 1f;

    private InputMaster m_input;

    private PlayerAnimationController m_anim;
    public static float directionEpsilon = 0.05f;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_input = new InputMaster();
        m_anim = GetComponent<PlayerAnimationController>();

        Debug.Log((m_anim == null));

        m_input.PlayerMovement.Jump.started += _ => Jump();
        m_input.PlayerMovement.WASD.started += ctx => MoveStart(ctx.ReadValue<Vector2>());
        m_input.PlayerMovement.WASD.canceled += _ => MoveEnd();

        m_input.PlayerMovement.Pickup.performed += _ => Pickup();

        string[] layers = new string[] { "Walkable" };
        walkable = LayerMask.GetMask(layers);

        layers = new string[] { "Walkable", "Plugs" };
        pickupable = LayerMask.GetMask(layers);
    }

    // Update is called once per frame
    private void Update()
    {
        // get input
        // update animations

        x_dir = m_velocity.x;

        m_anim.UpdateAnim(m_rb.velocity);

        if (m_velocity.y < 0.2)
        {
            CheckSurroundings();
        }
        if (m_pickedUp)
        {
            if (m_pickedUp.CompareTag("Plug"))
                m_pickedUp.transform.position = m_cableHoldPoint.position;
            else
                m_pickedUp.transform.position = m_HoldPoint.position;
            if (Mathf.Abs(m_velocity.x) >= directionEpsilon)
                m_pickedUp.transform.localScale = new Vector3(Mathf.Sign(m_velocity.x), 1, 1);
        }
    }

    private void FixedUpdate()
    {
        //if (m_velocity.x != 0)
        //{
        m_rb.velocity = new Vector2(m_maxSpeed * JUtil.JUtils.BetterSign(x_dir), m_rb.velocity.y);
        //}
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    private void Jump()
    {
        if (m_grounded)
        {
            m_grounded = false;
            m_anim.SetBool("isGrounded", false);
            m_anim.SetBool("isJumping", true);
            m_rb.AddForce(new Vector2(0, m_jumpHeight), ForceMode2D.Impulse);
        }
    }

    private void MoveStart(Vector2 in_velocity)
    {
        m_anim.SetBool("isRunning", true);
        m_velocity = in_velocity;
    }

    private void MoveEnd()
    {
        m_anim.SetBool("isRunning", false);
        m_velocity = new Vector2(0, 0);
    }

    private void Pickup()
    {
        if (!m_pickup)
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(m_pickupSensor.position, m_pickupSensorRadius, pickupable);

            foreach (Collider2D collide in collisions)
            {
                if (collide)
                {
                    if (collide.gameObject.CompareTag("Pickup") || collide.gameObject.CompareTag("Plug"))
                    {
                        m_pickedUp = collide.gameObject;
                        m_pickup = true;
                        m_pickedUp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                        bool playSound = true;
                        if (m_pickedUp.CompareTag("Plug"))
                        {
                            Nodes.NodeConnection node = m_pickedUp.GetComponent<Nodes.CablePlug>().node_out;

                            if (node.other != null)
                                playSound = false;

                            node.Disconnect();
                            m_anim.SetBool("isHoldingCable", true);
                            m_pickedUp.GetComponent<SpriteRenderer>().sortingOrder = 3;
                        }
                        else
                        {
                            m_anim.SetBool("isHolding", true);
                        }

                        // SOUND PICKUP
                        if (playSound)
                            bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/interactables/plugs/pick up");

                        if (collide.TryGetComponent<Rigidbody2D>(out var rb))
                        {
                            collide.GetComponent<BoxCollider2D>().isTrigger = true;
                            rb.freezeRotation = true;
                        }
                        break;
                    }
                }
            }
        }
        else
        {
            if (m_pickedUp.CompareTag("Plug"))
            {
                // try plug it in
                Nodes.NodeConnection conn = FindClosestInputConnector();

                if (conn != null)
                {
                    float dist = Vector3.Distance(conn.transform.position, transform.position);
                    if (dist < m_pickupRange)
                    {
                        if (conn.allowPlayerInteraction)
                        {
                            if (conn.Connect(m_pickedUp.GetComponent<Nodes.CablePlug>().node_out))
                            {
                                // success

                                m_pickedUp.transform.position = conn.transform.position;
                                m_pickedUp.GetComponent<SpriteRenderer>().sortingOrder = -2;

                                m_pickedUp = null;
                                m_pickup = false;
                                m_anim.SetBool("isHoldingCable", false);
                                return;
                            }
                            else
                            {
                                // failed to connect plug to connector
                            }
                        }
                    }
                }

                m_anim.SetBool("isHoldingCable", false);
            }
            else
            {
                m_anim.SetBool("isHolding", false);
            }

            if (m_pickedUp.TryGetComponent<Rigidbody2D>(out var rb))
            {
                m_pickedUp.GetComponent<BoxCollider2D>().isTrigger = false;
                rb.freezeRotation = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }

            // SOUND DROP
            StartCoroutine(ObjectDropNoises.DropObjectSound(m_pickedUp));

            m_pickedUp = null;
            m_pickup = false;

            // bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/interactables/plugs/put down");
        }

        Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, 0.5f, 0), 0.5f);

        foreach (Collider2D collide in collision)
        {
            if (collide.gameObject.CompareTag("Exit"))
            {
                var instance = bluModule.Application.instance;
                instance.settingsModule.saveData.SetLevelsComplete(instance.sceneModule.currentLevel);
                instance.sceneModule.SwitchScene("Level" + (instance.sceneModule.currentLevel + 1));
                instance.sceneModule.currentLevel++;
            }
        }
    }

    private void CheckSurroundings()
    {
        float spritewidth = GetComponent<BoxCollider2D>().bounds.extents.x;
        m_grounded = Physics2D.OverlapCircle(m_groundSensor.position, m_groundSensorRadius, walkable);
        m_grounded |= Physics2D.OverlapCircle(m_groundSensor.position + new Vector3((spritewidth * 0.6f), 0), m_groundSensorRadius, walkable);
        m_grounded |= Physics2D.OverlapCircle(m_groundSensor.position + new Vector3((spritewidth * 0.6f), 0), m_groundSensorRadius, walkable);

        if (m_grounded)
            m_anim.SetBool("isGrounded", true);
        else
            m_anim.SetBool("isGrounded", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        float bodyWidth = GetComponent<BoxCollider2D>().bounds.extents.x;

        Gizmos.DrawWireSphere(m_groundSensor.position, m_groundSensorRadius);
        Gizmos.DrawWireSphere(m_groundSensor.position + new Vector3(bodyWidth * 0.6f, 0), m_groundSensorRadius);
        Gizmos.DrawWireSphere(m_groundSensor.position - new Vector3(bodyWidth * 0.6f, 0), m_groundSensorRadius);
        Gizmos.DrawWireSphere(m_pickupSensor.position, m_pickupSensorRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.5f, 0), 0.5f);
    }

    private Nodes.NodeConnection FindClosestInputConnector()
    {
        Nodes.NodeConnection closest = null;
        float closestDist = 0f;

        for (int i = 0; i < NodeClock.Instance.m_Connectors.Count; i++)
        {
            float dist = Vector3.Distance(NodeClock.Instance.m_Connectors[i].transform.position, transform.position);

            if (closest == null)
            {
                if (m_pickedUp != NodeClock.Instance.m_Connectors[i].transform.parent.gameObject)
                {
                    closest = NodeClock.Instance.m_Connectors[i];
                    closestDist = dist;
                }
            }

            if (dist < closestDist)
            {
                if (m_pickedUp != NodeClock.Instance.m_Connectors[i].transform.parent.gameObject)
                {
                    closestDist = dist;
                    closest = NodeClock.Instance.m_Connectors[i];
                }
            }
        }

        return closest;
    }
}