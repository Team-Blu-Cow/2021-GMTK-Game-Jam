using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[Serializable]
public class ButtonContainer
{
    public List<Canvas> canvas;

    public Button button;
    public Image image;
    public TextMeshProUGUI textMeshPro;
    public GameObject gameObject;

    public bool open;
    public bool quit;
    public bool swapScene;
    public string sceneName = "";

    public bool stack;

    public string text;
    public string name;

    public bool showInEditor = false;
}