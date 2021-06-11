using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int m_speed;

    [SerializeField]
    [Range(0, 100)]
    private int m_jumpHeight;

    private Vector2 m_velocity;
    private Rigidbody2D m_rb;

    private InputMaster m_input;

    private void Awake()
    {
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
        if (m_rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(m_rb.velocity.x), transform.localScale.y);
        }
    }

    private void FixedUpdate()
    {
        if (m_velocity.x != 0)
        {
            m_rb.AddForce(m_velocity * m_speed * Time.deltaTime);
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
        m_rb.AddForce(new Vector2(0, m_jumpHeight), ForceMode2D.Impulse);
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
        m_velocity = new Vector2(0, 0);
    }
}