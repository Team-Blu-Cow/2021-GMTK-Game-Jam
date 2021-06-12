using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CanvasTool
{
    public class CanvasManager : MonoBehaviour
    {
        // List of all the canvases in the scene
        [SerializeField]
        private List<CanvasContainer> canvases;

        public Camera mainCamera;

        public List<string> layerNames;

        private CanvasContainer overlay = new CanvasContainer();
        public CanvasContainer startingCanvas;

        // Stack of open canvases
        private Stack<CanvasContainer> openCanvases = new Stack<CanvasContainer>();

        public Vector2 refrenenceResolution = new Vector2(1600, 900);

        private void Awake()
        {
            if (canvases == null)
            {
                canvases = new List<CanvasContainer>();
            }

            if (layerNames == null)
            {
                layerNames = new List<string>();
                layerNames.Add("Default");
            }

            if (overlay.gameObject == null)
            {
                GameObject Go = new GameObject("Overlay");

                Go.transform.SetParent(transform);

                // Add compenents to the game Object
                Canvas canvas = Go.AddComponent<Canvas>();
                CanvasScaler canvasScaler = Go.AddComponent<CanvasScaler>();
                Go.AddComponent<GraphicRaycaster>();
                Image image = Go.AddComponent<Image>();

                // Set up added components
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.enabled = false;

                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = refrenenceResolution;

                image.color = new Color(0, 0, 0, 0.7f);

                overlay.canvas = canvas;
                overlay.canvasScaler = canvasScaler;
                overlay.gameObject = Go;
            }

            CloseCanvas(true);
            OpenCanvas(startingCanvas);
        }

        public void OpenCanvas(CanvasContainer container, bool stack = false)
        {
            //overlay.canvas.enabled = true;

            // If the canvas is already open
            if (openCanvases.Contains(container))
            {
                // Close untill at desired canvas
                while (openCanvases.Peek() != container)
                {
                    // Close canvases
                    CanvasContainer top = openCanvases.Pop();
                    top.CloseCanvas();
                }
                overlay.canvas.sortingOrder = openCanvases.Count;
                return;
            }

            if (stack)
            {
                if (openCanvases.Count > 0 && openCanvases.Peek().layer == container.layer)
                {
                    openCanvases.Pop();
                }

                //close on same layer
                if (container.layer != 0)
                {
                    foreach (CanvasContainer canvas in canvases)
                    {
                        if (container.layer == canvas.layer)
                        {
                            canvas.CloseCanvas();
                        }
                    }
                }

                openCanvases.Push(container);
            }
            else
            {
                // Close canvases
                foreach (CanvasContainer canvas in canvases)
                {
                    canvas.CloseCanvas();
                }
            }

            container.OpenCanvas();
            container.canvas.sortingOrder = openCanvases.Count;
            overlay.canvas.sortingOrder = openCanvases.Count;
        }

        public void CloseCanvas(bool all = false)
        {
            if (all)
            {
                foreach (CanvasContainer canvas in canvases)
                {
                    canvas.CloseCanvas();
                }
            }
            else
            {
                CanvasContainer top = openCanvases.Pop();
                top.CloseCanvas();
                overlay.canvas.sortingOrder = openCanvases.Count - 1;
            }
        }

        public void MoveUp(int index)
        {
            if (index - 1 >= 0)
            {
                CanvasContainer temp = GetCanvasContainer(index);
                canvases.RemoveAt(index);
                canvases.Insert(index - 1, temp);
            }
        }

        public void MoveDown(int index)
        {
            if (index + 1 < canvases.Count)
            {
                CanvasContainer temp = GetCanvasContainer(index);
                canvases.RemoveAt(index);
                canvases.Insert(index + 1, temp);
            }
        }

        public Canvas GetCanvasIndex(int in_index)
        {
            if (in_index <= canvases.Count)
                return canvases[in_index].canvas;
            else
                return null;
        }

        #region GetContainer

        public CanvasContainer GetCanvasContainer(string in_name)
        {
            return canvases.Find(i => i.name == in_name);
        }

        public CanvasContainer GetCanvasContainer(Canvas in_canvas)
        {
            return canvases.Find(i => i.canvas == in_canvas);
        }

        public CanvasContainer GetCanvasContainer(int in_index)
        {
            if (in_index < canvases.Count)
                return canvases[in_index];
            else
                return null;
        }

        #endregion GetContainer

        #region RemoveCanvas

        public bool RemoveCanvasContainer(string name)
        {
            CanvasContainer temp = GetCanvasContainer(name);
            CleanUpContainer(temp);
            return canvases.Remove(temp);
        }

        public bool RemoveCanvasContainer(Canvas canvas)
        {
            CanvasContainer temp = GetCanvasContainer(canvas);
            CleanUpContainer(temp);
            return canvases.Remove(temp);
        }

        public bool RemoveCanvasContainer(int index)
        {
            CleanUpContainer(GetCanvasContainer(index));
            if (index <= canvases.Count)
            {
                canvases.RemoveAt(index);
                return true;
            }
            return false;
        }

        #endregion RemoveCanvas

        public int CanvasAmount()
        {
            return canvases.Count;
        }

        // Add a new canvas to the scene
        public void AddCanvas()
        {
            CanvasContainer canvasContainer = new CanvasContainer();

            // make a new game object and setup
            GameObject Go = new GameObject("Canvas");
            Go.transform.SetParent(transform);

            // Add compenents to the game Object
            Canvas canvas = Go.AddComponent<Canvas>();
            CanvasScaler canvasScaler = Go.AddComponent<CanvasScaler>();
            Go.AddComponent<GraphicRaycaster>();
            Go.AddComponent<ButtonWrapper>();

            // Set up added components
            canvas.renderMode = RenderMode.ScreenSpaceCamera;

            if (mainCamera)
                canvas.worldCamera = mainCamera;
            else
                canvas.worldCamera = Camera.main;

            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = refrenenceResolution;

            canvasContainer.canvas = canvas;
            canvasContainer.canvasScaler = canvasScaler;
            canvasContainer.gameObject = Go;

            canvases.Add(canvasContainer);
        }

        // Deletes the game object of the canvas
        private void CleanUpContainer(CanvasContainer container)
        {
            if (container.canvas)
                DestroyImmediate(container.canvas.gameObject);
        }

        public void AddLayer(string s)
        {
            if (!layerNames.Contains(s))
            {
                layerNames.Add(s);
            }
            else
                Debug.LogWarning("Cant add a layer with the same name");
        }

        public void RemoveLayer(int index)
        {
            layerNames.RemoveAt(index);
        }
    }
}