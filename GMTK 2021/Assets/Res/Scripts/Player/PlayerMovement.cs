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
    private Vector2 m_velocity;
    private Rigidbody2D m_rb;

    private InputMaster m_input;

    public PlayerAnimationController m_anim;

    private void Awake()
    {
        m_inAir = true;
        m_rb = GetComponent<Rigidbody2D>();
        m_input = new InputMaster();
        m_anim = GetComponent<PlayerAnimationController>();

        Debug.Log((m_anim == null));

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
        m_anim.UpdateAnim(m_rb.velocity);
    }

    private void FixedUpdate()
    {
        if (m_velocity.x != 0)
        {
            m_rb.velocity = new Vector2(m_maxSpeed * Mathf.Sign(m_velocity.x), m_rb.velocity.y);

            //m_rb.AddForce(m_velocity * m_speed * Time.deltaTime);
            //if (Mathf.Abs(m_rb.velocity.x) > m_maxSpeed)
            //{
            //    m_rb.velocity = new Vector2(m_maxSpeed * Mathf.Sign(m_rb.velocity.x), m_rb.velocity.y);
            //}
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
        if (!m_pickup)
        {
            m_pickup = true;
        }
        else
        {
            m_pickup = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            m_inAir = false;
            m_anim.SetBool("isGrounded", true);
        }
    }
}