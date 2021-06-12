using UnityEngine;
using UnityEditor;

namespace CanvasTool
{
    [CustomEditor(typeof(CanvasManager))]
    public class CanvasEditor : Editor
    {
        private int indentSize = 20;
        [SerializeField] private bool showLayers;
        private string layerName = "";

        private static GUIContent
            moveDownButtonContent = new GUIContent("\u2193", "Move Down"),
            moveUpButtonContent = new GUIContent("\u2191", "Move Up"),
            addButtonContent = new GUIContent("+", "Add"),
            deleteButtonContent = new GUIContent("-", "Delete");

        public override void OnInspectorGUI()
        {
            CanvasManager canvasContoller = (CanvasManager)target;

            GUILayout.Label("Canvases", "BoldLabel");

            using (var HorizontalScope = new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Default Resolution", GUILayout.Width(110));
                Indent();
                canvasContoller.refrenenceResolution = EditorGUILayout.Vector2Field("", canvasContoller.refrenenceResolution);
            }

            using (var HorizontalScope = new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Starting Canvas", GUILayout.Width(110));
                Indent();
                canvasContoller.startingCanvas.canvas = (Canvas)EditorGUILayout.ObjectField(canvasContoller.startingCanvas.canvas, typeof(Canvas), true);
            }

            using (var HorizontalScope = new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Main Camera", GUILayout.Width(110));
                Indent();
                canvasContoller.mainCamera = (Camera)EditorGUILayout.ObjectField(canvasContoller.mainCamera, typeof(Camera), true);
            }

            for (int i = 0; i < canvasContoller.CanvasAmount(); i++)
            {
                CanvasContainer currentCanvas = canvasContoller.GetCanvasContainer(i);

                if (currentCanvas != null)
                {
                    using (var HorizontalScope = new GUILayout.HorizontalScope())
                    {
                        currentCanvas.showInEditor = EditorGUILayout.Foldout(currentCanvas.showInEditor, "Canvas " + (i + 1) + " (" + currentCanvas.name + ")", true);

                        if (!currentCanvas.showInEditor)
                        {
                            if (GUILayout.Button(moveDownButtonContent, EditorStyles.miniButtonLeft))
                            {
                                canvasContoller.MoveDown(i);
                            }

                            if (GUILayout.Button(moveUpButtonContent, EditorStyles.miniButtonMid))
                            {
                                canvasContoller.MoveUp(i);
                            }

                            if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight))
                            {
                                canvasContoller.RemoveCanvasContainer(i);
                            }
                        }
                    }

                    if (currentCanvas.showInEditor)
                    {
                        using (var VerticleScope = new GUILayout.VerticalScope("HelpBox"))
                        {
                            GUILayout.Space(16);

                            using (var HorizontalScope = new GUILayout.HorizontalScope())
                            {
                                Indent();
                                GUILayout.Label("Layer", GUILayout.Width(100));
                                currentCanvas.layer = EditorGUILayout.Popup(currentCanvas.layer, canvasContoller.layerNames.ToArray());
                            }

                            using (var HorizontalScope = new GUILayout.HorizontalScope())
                            {
                                Indent();
                                GUILayout.Label("Name", GUILayout.Width(100));
                                GUI.changed = false;
                                currentCanvas.name = GUILayout.TextField(currentCanvas.name, 50);
                                if (GUI.changed)
                                {
                                    currentCanvas.gameObject.name = currentCanvas.name;
                                }
                            }

                            using (var HorizontalScope = new GUILayout.HorizontalScope())
                            {
                                Indent();
                                GUILayout.Label("Description", GUILayout.Width(100));
                                currentCanvas.desc = GUILayout.TextField(currentCanvas.desc, 50);
                            }

                            using (var HorizontalScope = new GUILayout.HorizontalScope())
                            {
                                Indent();
                                GUILayout.Label("Canvas", GUILayout.Width(100));

                                EditorGUILayout.ObjectField(currentCanvas.canvas, typeof(Canvas), true);
                            }

                            using (var HorizontalScope = new GUILayout.HorizontalScope())
                            {
                                Indent();
                                GUILayout.Label("Resolution", GUILayout.Width(100));
                                currentCanvas.canvasScaler.referenceResolution = EditorGUILayout.Vector2Field("", currentCanvas.canvasScaler.referenceResolution);
                            }

                            using (var HorizontalScope = new GUILayout.HorizontalScope())
                            {
                                Indent();
                                GUILayout.Label("Transition", GUILayout.Width(100));
                                currentCanvas.transition = EditorGUILayout.Popup(currentCanvas.transition, currentCanvas.transitions);
                            }

                            Indent();

                            GUILayout.Label("Buttons");
                            CreateEditor(currentCanvas.gameObject.GetComponent<ButtonWrapper>()).OnInspectorGUI();

                            Indent(10);

                            using (var HorizontalScope = new GUILayout.HorizontalScope())
                            {
                                if (GUILayout.Button(moveDownButtonContent, EditorStyles.miniButtonLeft))
                                {
                                    canvasContoller.MoveDown(i);
                                }

                                if (GUILayout.Button(moveUpButtonContent, EditorStyles.miniButtonMid))
                                {
                                    canvasContoller.MoveUp(i);
                                }

                                if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight))
                                {
                                    canvasContoller.RemoveCanvasContainer(i);
                                }
                            }

                            Indent(2);
                        }
                    }
                }
            }

            if (GUILayout.Button(addButtonContent))
            {
                canvasContoller.AddCanvas();
            }

            Indent(5);

            showLayers = EditorGUILayout.Foldout(showLayers, "Layers", true);

            if (showLayers)
            {
                int i = 0;
                foreach (string s in canvasContoller.layerNames)
                {
                    using (var VerticleScope = new GUILayout.VerticalScope("HelpBox"))
                    {
                        GUILayout.Label(s);

                        Indent(5);

                        // Kinda bad here, would liek to store them in a list or somethign but didnt seem to work
                        // This will work for now but coudl be umprved to be more efficiant
                        for (int j = 0; j < canvasContoller.CanvasAmount(); j++)
                        {
                            CanvasContainer currentCanvas = canvasContoller.GetCanvasContainer(j);

                            if (currentCanvas.layer == i)
                            {
                                using (var HorizontalScope = new GUILayout.HorizontalScope())
                                {
                                    Indent();
                                    GUILayout.Label(currentCanvas.name, GUILayout.Width(100));
                                    currentCanvas.layer = EditorGUILayout.Popup(currentCanvas.layer, canvasContoller.layerNames.ToArray());
                                }
                            }
                        }

                        if (s != "Default")
                        {
                            using (var HorizontalScope = new GUILayout.HorizontalScope())
                            {
                                if (GUILayout.Button(moveDownButtonContent, EditorStyles.miniButtonLeft))
                                {
                                }

                                if (GUILayout.Button(moveUpButtonContent, EditorStyles.miniButtonMid))
                                {
                                }

                                if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight))
                                {
                                    canvasContoller.RemoveLayer(i);
                                    break;
                                }
                            }
                        }
                        i++;
                    }
                }

                Indent();

                using (var HorizontalScope = new GUILayout.HorizontalScope())
                {
                    layerName = GUILayout.TextField(layerName, 50, GUILayout.Width(300));

                    Indent();

                    if (GUILayout.Button(addButtonContent, EditorStyles.miniButtonMid))
                    {
                        canvasContoller.AddLayer(layerName);
                    }
                }
            }
        }

        private void Indent()
        {
            GUILayout.Space(indentSize);
        }

        private void Indent(int size)
        {
            GUILayout.Space(size);
        }
    }
}