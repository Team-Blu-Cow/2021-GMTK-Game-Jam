using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    protected bool m_powered = false;
    private InputMaster m_input;
    public bool m_active = true;

    private void Awake()
    {
        m_input = new InputMaster();

        m_input.PlayerMovement.MouseLClick.started += ctx => MouseDown();
        UnPower();
    }

    // Update is called once per frame
    private void Update()
    {
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
                    if (m_powered)
                    {
                        Clicked();
                    }
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/UI/buttons/on click");
                }
                else
                {
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/UI/buttons/denied");
                }
            }
        }
    }

    public void Power()
    {
        if (m_active)
        {
            m_powered = true;

            GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/nodes/button insert");
        }
    }

    public void UnPower()
    {
        if (m_powered)
        {
            m_powered = false;
            GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/nodes/button remove");
        }
    }

    protected virtual void Clicked()
    {
        Debug.Log("Clicked");
    }
}