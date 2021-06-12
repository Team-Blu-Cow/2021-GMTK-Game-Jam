using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlugMoving : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private InputMaster m_input;

    [SerializeField]
    private Transform m_pickupSensor;

    public float m_pickupRange;

    private Vector3 previousPos;
    private float previousAngle;

    private bool m_clicked = false;

    private void Awake()
    {
        m_input = new InputMaster();
    }

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 v3BottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));    // scaling like this
        Vector3 v3TopRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));      // only works with
                                                                                       // sprites that are
        float xScale = v3TopRight.x - v3BottomLeft.x;                                  // 1 unit.
        float yScale = v3TopRight.y - v3BottomLeft.y;                                  //
                                                                                       //
                                                                                       //
        if (xScale < yScale)                                                           //
            transform.localScale = new Vector2(xScale * 0.25f, xScale * 0.25f);   //
        else                                                                           //
            transform.localScale = new Vector2(yScale * 0.25f, yScale * 0.25f);   //
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_clicked)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(m_input.PlayerMovement.MousePosition.ReadValue<Vector2>());
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);

            if (transform.position != previousPos)
            {
                float angle = Vector2.SignedAngle(new Vector2(1, 0), transform.position - previousPos);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 0.1f);
            }

            Collider2D overlap = Physics2D.OverlapBox(m_pickupSensor.position, new Vector2(m_pickupRange, 1), 0);
            if (overlap)
            {
                transform.position = new Vector3(overlap.transform.position.x - ((1 * overlap.transform.localScale.x) / 2), overlap.transform.position.y, overlap.transform.position.z);
                transform.rotation = Quaternion.identity;
            }

            previousPos = transform.position;
        }
        else
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            Vector2 topRight = new Vector2(rectTransform.position.x + rectTransform.rect.width / 2, transform.position.y + rectTransform.rect.height / 2);
            Vector2 bottomLeft = new Vector2(rectTransform.position.x - rectTransform.rect.width / 2, transform.position.y - rectTransform.rect.height / 2);

            Collider2D[] overlap = Physics2D.OverlapAreaAll(topRight, bottomLeft);

            if (overlap.Length > 0)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();

                rb.velocity = new Vector2(rb.velocity.x, 0);
                transform.position = previousPos;
            }
        }
    }

    private void OnEnable()
    {
        m_input.Enable(); ;
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Plug"))
        {
            m_clicked = true;

            Collider2D overlap = Physics2D.OverlapBox(m_pickupSensor.position, new Vector2(m_pickupRange, 1), 0);
            if (overlap && overlap.CompareTag("MenuButton"))
            {
                overlap.GetComponent<MenuButton>().UnPower();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_clicked = false;
        Collider2D overlap = Physics2D.OverlapBox(m_pickupSensor.position, new Vector2(m_pickupRange, 1), 0);
        if (overlap && overlap.CompareTag("MenuButton"))
        {
            overlap.GetComponent<MenuButton>().Power();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(m_pickupSensor.position, new Vector3(m_pickupRange, 1f, 0));
    }
}