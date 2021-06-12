using UnityEngine;
using UnityEngine.UI;
using System;

namespace CanvasTool
{
    [Serializable]
    public class CanvasContainer
    {
        public GameObject gameObject;
        public Canvas canvas;
        public CanvasScaler canvasScaler;

        public string name = "Canvas";
        public string desc = "";
        public int layer = 0;
        public int transition = 0;
        public bool showInEditor = false;

        public string[] transitions = new string[] { "0", "1", "2" };

        virtual public void CloseCanvas()
        {
            canvas.enabled = false;
        }

        virtual public void OpenCanvas()
        {
            if (canvas)
                canvas.enabled = true;
        }
    }
}