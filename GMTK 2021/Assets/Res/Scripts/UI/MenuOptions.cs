using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MenuButton
{
    protected override void Clicked()
    {
        CanvasTool.CanvasManager canvasManager = FindObjectOfType<CanvasTool.CanvasManager>();

        List<CanvasTool.CanvasContainer> canvases = new List<CanvasTool.CanvasContainer>();

        canvases.Add(canvasManager.GetCanvasContainer("Options"));
        canvases.Add(canvasManager.GetCanvasContainer("Graphics"));

        canvasManager.OpenCanvas(canvases, true);
        GetComponentInParent<HideButtons>().CloseAll();
    }
}