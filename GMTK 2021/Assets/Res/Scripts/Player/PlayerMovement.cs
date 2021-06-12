using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int m_maxSpeed;

    [SerializeField]
    [Range(0, 100)]
    private int m_jumpHeight;

    private bool m_inAir;
    private bool m_pickup;
    private GameObject m_pickedUp;

    private Vector2 m_velocity;
    private Rigidbody2D m_rb;

    private float m_pickupRange = 1f;

    private InputMaster m_input;

    private void Awake()
    {
        m_inAir = true;
        m_rb = GetComponent<Rigidbody2D>();
        m_input = new InputMaster();

        m_input.PlayerMovement.Jump.started += _ => Jump();
        m_input.PlayerMovement.WASD.started += ctx => MoveStart(ctx.ReadValue<Vector2>());
        m_input.PlayerMovement.WASD.canceled += _ => MoveEnd();

        m_input.PlayerMovement.Pickup.performed += _ => Pickup();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_rb.velocity.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(m_rb.velocity.x), 1, 1);
        }

        if (m_pickedUp)
        {
            m_pickedUp.transform.position = transform.position;
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
        if (!m_inAir)
        {
            m_inAir = true;
            m_rb.AddForce(new Vector2(0, m_jumpHeight), ForceMode2D.Impulse);
        }
    }

    private void MoveStart(Vector2 in_velocity)
    {
        m_velocity = in_velocity;
    }

    private void MoveEnd()
    {
        m_velocity = new Vector2(0, 0);
    }

    private void Pickup()
    {
        if (!m_pickup)
        {
            Collider2D[] collisions = new Collider2D[10];

            m_rb.OverlapCollider(new ContactFilter2D().NoFilter(), collisions);

            foreach (Collider2D collide in collisions)
            {
                if (collide)
                {
                    if (collide.gameObject.CompareTag("Pickup") || collide.gameObject.CompareTag("Plug"))
                    {
                        m_pickedUp = collide.gameObject;
                        m_pickup = true;

                        if(m_pickedUp.CompareTag("Plug"))
                        {
                            m_pickedUp.GetComponentInChildren<Nodes.OutputConnection>().Disconnect();
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
                Nodes.InputConnection conn = FindClosestInputConnector();

                if (conn != null)
                {
                    float dist = Vector3.Distance(conn.transform.position, transform.position);
                    if (dist < m_pickupRange)
                    {
                        if (conn.Connect(m_pickedUp.GetComponentInChildren<Nodes.OutputConnection>()))
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            m_inAir = false;
        }
    }

    private Nodes.InputConnection FindClosestInputConnector()
    {
        Nodes.InputConnection closest = null;
        float closestDist = 0f;

        for (int i = 0; i < NodeClock.Instance.m_allInputConnectors.Count; i++)
        {
            float dist = Vector3.Distance(NodeClock.Instance.m_allInputConnectors[i].transform.position, transform.position);

            if (closest == null)
            {
                if (m_pickedUp != NodeClock.Instance.m_allInputConnectors[i].transform.parent.gameObject)
                {
                    closest = NodeClock.Instance.m_allInputConnectors[i];
                    closestDist = dist;
                }
            }

            if (dist < closestDist)
            {
                if (m_pickedUp != NodeClock.Instance.m_allInputConnectors[i].transform.parent.gameObject)
                {
                    closestDist = dist;
                    closest = NodeClock.Instance.m_allInputConnectors[i];
                }
            }
        }

        return closest;
    }
}