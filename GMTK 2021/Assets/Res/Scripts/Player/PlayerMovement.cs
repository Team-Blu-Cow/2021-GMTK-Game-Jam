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

    
    [SerializeField]
    private Transform m_pickupSensor;

    [SerializeField]
    private float m_pickupSensorRadius;

    [Header("Transforms")]
    [SerializeField]
    private Transform m_cableHoldPoint;

    private bool m_grounded = false;
    private bool m_pickup;
    private GameObject m_pickedUp;

    private Vector2 m_velocity;
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

        walkable = LayerMask.GetMask("Walkable");
    }

    // Update is called once per frame
    private void Update()
    {
        m_anim.UpdateAnim(m_rb.velocity);

        if (m_velocity.y < 0.2)
        {
            CheckSurroundings();
        }
        if (m_pickedUp)
        {
            m_pickedUp.transform.position = m_cableHoldPoint.position;
            if (Mathf.Abs(m_velocity.x) >= directionEpsilon)
                m_pickedUp.transform.localScale = new Vector3(Mathf.Sign(m_velocity.x), 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (m_velocity.x != 0)
        {
            m_rb.velocity = new Vector2(m_maxSpeed * Mathf.Sign(m_velocity.x), m_rb.velocity.y);
        }
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
        bluModule.Application.instance.sceneModule.SwitchScene("SampleScene");

        if (!m_pickup)
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(m_pickupSensor.position, m_pickupSensorRadius, walkable);

            foreach (Collider2D collide in collisions)
            {
                if (collide)
                {
                    if (collide.gameObject.CompareTag("Pickup") || collide.gameObject.CompareTag("Plug"))
                    {
                        m_pickedUp = collide.gameObject;
                        m_pickup = true;
                        m_pickedUp.GetComponent<BoxCollider2D>().enabled = false;

                        if (m_pickedUp.CompareTag("Plug"))
                        {
                            m_pickedUp.GetComponentInChildren<Nodes.NodeConnection>().Disconnect();
                            m_anim.SetBool("isHoldingCable", true);
                        }

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
                        if (conn.Connect(m_pickedUp.GetComponentInChildren<Nodes.NodeConnection>()))
                        {
                            // success

                            m_pickedUp.transform.position = conn.transform.position;

                            m_pickedUp = null;
                            m_pickup = false;
                            
                            return;
                        }
                        else
                        {
                            // fail
                        }
                    }
                }

                m_anim.SetBool("isHoldingCable", false);
                m_pickedUp.GetComponent<BoxCollider2D>().enabled = true;

            }

            if (m_pickedUp.TryGetComponent<Rigidbody2D>(out var rb))
            {
                m_pickedUp.GetComponent<BoxCollider2D>().isTrigger = false;
                rb.freezeRotation = false;
            }
            m_pickedUp = null;
            m_pickup = false;
        }
    }

    private void CheckSurroundings()
    {
        m_grounded = Physics2D.OverlapCircle(m_groundSensor.position, m_groundSensorRadius, walkable);
        if (m_grounded)
            m_anim.SetBool("isGrounded", true);
        else
            m_anim.SetBool("isGrounded", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(m_groundSensor.position, m_groundSensorRadius);
        Gizmos.DrawWireSphere(m_pickupSensor.position, m_pickupSensorRadius);
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