using UnityEngine;

public class HideButtons : MonoBehaviour
{
    private CanvasTool.CanvasManager canvasManager;

    private void Start()
    {
        canvasManager = FindObjectOfType<CanvasTool.CanvasManager>();
        OpenAll();
    }

    public void CloseAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        //canvasManager.CloseCanvas();
    }

    public void OpenAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        canvasManager.OpenCanvas(canvasManager.GetCanvasContainer("Buttons"));
    }
}