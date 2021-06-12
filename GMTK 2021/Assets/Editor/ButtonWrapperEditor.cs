using UnityEngine;
using UnityEditor;
using CanvasTool;

[CustomEditor(typeof(ButtonWrapper))]
public class ButtonWrapperEditor : Editor
{
    private static GUIContent
        moveDownButtonContent = new GUIContent("\u2193", "Move Down"),
        moveUpButtonContent = new GUIContent("\u2191", "Move Up"),
        addButtonContent = new GUIContent("+", "Add"),
        deleteButtonContent = new GUIContent("-", "Delete");

    // List of all the canvases in the scene
    [SerializeField]
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        ButtonWrapper buttonWrapper = (ButtonWrapper)target;

        CanvasManager canvasManager = FindObjectOfType<CanvasManager>();

        int i = 0;
        foreach (ButtonContainer button in buttonWrapper.buttons)
        {
            button.showInEditor = EditorGUILayout.Foldout(button.showInEditor, "Button " + (i + 1) + " (" + button.name + ")", true);

            if (button.showInEditor)
            {
                using (var VericalScope = new GUILayout.VerticalScope("HelpBox"))
                {
                    using (var HorizontalScope = new GUILayout.HorizontalScope())
                    {
                        Indent();

                        GUILayout.Label("Game Object", GUILayout.Width(100));
                        EditorGUILayout.ObjectField(button.gameObject, typeof(GameObject), true);
                    }

                    using (var HorizontalScope = new GUILayout.HorizontalScope())
                    {
                        Indent();
                        GUI.changed = false;

                        GUILayout.Label("Button Name", GUILayout.Width(100));
                        button.name = GUILayout.TextField(button.name);

                        if (GUI.changed)
                        {
                            button.button.gameObject.name = button.name;
                        }
                    }

                    using (var HorizontalScope = new GUILayout.HorizontalScope())
                    {
                        Indent();
                        GUI.changed = false;

                        GUILayout.Label("Button Text", GUILayout.Width(100));
                        button.text = GUILayout.TextField(button.text);

                        if (GUI.changed)
                        {
                            button.textMeshPro.text = button.text;
                        }
                    }

                    using (var HorizontalScope = new GUILayout.HorizontalScope())
                    {
                        Indent();
                        GUILayout.Label("Sprite", GUILayout.Width(100));
                        Sprite sprite = button.image.sprite;
                        button.image.sprite = (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), true);
                    }

                    using (var VerticalScope = new GUILayout.VerticalScope())
                    {
                        for (int x = 0; x < button.canvas.Count; x++)
                        {
                            using (var HorizontalScope = new GUILayout.HorizontalScope())
                            {
                                Indent();
                                GUILayout.Label("Swap Canvas", GUILayout.Width(100));
                                button.canvas[x] = (Canvas)EditorGUILayout.ObjectField(button.canvas[x], typeof(Canvas), true);
                                if (GUILayout.Button(deleteButtonContent))
                                {
                                    button.canvas.Remove(button.canvas[x]);
                                }
                            }
                        }

                        using (var HorizontalScope = new GUILayout.HorizontalScope())
                        {
                            Indent();
                            GUILayout.Label("Add Swap Canvas", GUILayout.Width(120));

                            if (GUILayout.Button(addButtonContent))
                            {
                                button.canvas.Add(new Canvas());
                            }
                        }
                    }

                    Indent();

                    using (var HorizontalScope = new GUILayout.HorizontalScope())
                    {
                        Indent();
                        GUI.changed = false;
                        button.swapScene = GUILayout.Toggle(button.swapScene, "SwapScene");
                        if (button.swapScene)
                        {
                            button.sceneName = GUILayout.TextField(button.sceneName);
                            if (Application.isPlaying && GUI.changed)
                            {
                                button.button.onClick.RemoveAllListeners();
                                button.button.onClick.AddListener(delegate { bluModule.Application.instance.audioModule.PlayAudioEvent("event:/UI/buttons/on click"); });

                                button.button.onClick.AddListener(delegate { bluModule.Application.instance.sceneModule.SwitchScene(button.sceneName); });
                            }
                        }

                        if (!button.swapScene)
                        {
                            button.quit = GUILayout.Toggle(button.quit, "Quit");

                            if (Application.isPlaying && GUI.changed && button.quit)
                            {
                                button.button.onClick.RemoveAllListeners();
                                button.button.onClick.AddListener(delegate { bluModule.Application.instance.audioModule.PlayAudioEvent("event:/UI/buttons/on click"); });

                                button.button.onClick.AddListener(delegate { bluModule.Application.instance.sceneModule.Quit(); });
                            }
                        }
                        else
                        {
                            button.quit = false;
                        }
                    }

                    using (var HorizontalScope = new GUILayout.HorizontalScope())
                    {
                        Indent();
                        if (!button.quit)
                        {
                            if (!button.swapScene)
                            {
                                button.open = GUILayout.Toggle(button.open, "Open");
                                if (button.open)
                                {
                                    button.stack = GUILayout.Toggle(button.stack, "Stack");
                                }
                                else
                                {
                                    button.stack = false;
                                }

                                if (Application.isPlaying && GUI.changed)
                                {
                                    button.button.onClick.RemoveAllListeners();
                                    button.button.onClick.AddListener(delegate { bluModule.Application.instance.audioModule.PlayAudioEvent("event:/UI/buttons/on click"); });

                                    if (button.open)
                                        button.button.onClick.AddListener(delegate { canvasManager.OpenCanvas(canvasManager.GetCanvasContainer(button.canvas[0]), button.stack); });
                                    else
                                        button.button.onClick.AddListener(delegate { canvasManager.CloseCanvas(button.stack); });
                                }
                            }
                            else
                            {
                                button.open = false;
                            }
                        }
                        else
                        {
                            button.open = false;
                        }
                    }

                    if (GUILayout.Button(deleteButtonContent))
                    {
                        buttonWrapper.RemoveButton(button);
                        break;
                    }
                }
                i++;
            }
        }

        if (GUILayout.Button(addButtonContent))
        {
            buttonWrapper.AddButton();
        }
    }

    private void Indent()
    {
        GUILayout.Space(20);
    }

    private void Indent(int size)
    {
        GUILayout.Space(size);
    }
}