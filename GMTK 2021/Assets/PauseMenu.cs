using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private InputMaster m_input;

    private void Awake()
    {
        m_input = new InputMaster();
        m_input.PlayerMovement.Pause.performed += _ => Pause();
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void OnDisable()
    {
        m_input.Disable();
    }

    private void Pause()
    {
        CanvasTool.CanvasManager canvasManager = FindObjectOfType<CanvasTool.CanvasManager>();
        canvasManager.OpenCanvas(canvasManager.GetCanvasContainer("Pause"));
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }
}