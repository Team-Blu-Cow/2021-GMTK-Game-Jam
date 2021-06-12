using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int m_maxSpeed;

    [SerializeField]
    private Transform m_groundSensor;

    [SerializeField]
    private float m_groundSensorRadius;

    private LayerMask walkable;

    [SerializeField]
    private Transform m_pickupSensor;

    [SerializeField]
    private float m_pickupSensorRadius;

    [SerializeField]
    [Range(0, 100)]
    private int m_jumpHeight;

    private bool m_grounded = false;
    private bool m_pickup;
    private GameObject m_pickedUp;

    private Vector2 m_velocity;
    private Rigidbody2D m_rb;

    private InputMaster m_input;

    public PlayerAnimationController m_anim;

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

        if (m_rb.velocity.y < 0.2)
            CheckSurroundings();

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
                    if (collide.gameObject.CompareTag("Pickup"))
                    {
                        m_pickedUp = collide.gameObject;
                        m_pickup = true;

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(m_groundSensor.position, m_groundSensorRadius);
        Gizmos.DrawWireSphere(m_pickupSensor.position, m_pickupSensorRadius);
    }
}