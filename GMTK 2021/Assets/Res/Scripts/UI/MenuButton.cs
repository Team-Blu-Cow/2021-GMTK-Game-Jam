using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    protected bool m_powered = false;
    private float m_clock = 0;
    private InputMaster m_input;

    private void Awake()
    {
        m_input = new InputMaster();

        m_input.PlayerMovement.MouseLClick.canceled += ctx => MouseDown();
        UnPower();
    }

    // Update is called once per frame
    private void Update()
    {
        m_clock += Time.deltaTime;
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    private void MouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(m_input.PlayerMovement.MousePosition.ReadValue<Vector2>());
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("MenuButton"))
            {
                if (hit.collider.GetComponent<MenuButton>().m_powered)
                {
                    Clicked();
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/UI/buttons/on click");
                }
                else
                {
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/UI/buttons/on click");
                }
            }
        }
    }

    public void Power()
    {
        m_powered = true;
        GetComponent<SpriteRenderer>().color = Color.green;
        bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/nodes/button insert");
    }

    public void UnPower()
    {
        if (m_powered)
        {
            m_powered = false;
            GetComponent<SpriteRenderer>().color = Color.red;
            bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/nodes/button remove");
        }
    }

    protected virtual void Clicked()
    {
        if (m_powered)
            Debug.Log("Clicked");
    }
}