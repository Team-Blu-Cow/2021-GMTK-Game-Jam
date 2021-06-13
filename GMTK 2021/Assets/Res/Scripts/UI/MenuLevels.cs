using TMPro;
using UnityEngine;

public class MenuLevels : MenuButton
{
    private CanvasTool.CanvasManager canvasManager;
    [SerializeField] private bool next;

    private void Start()
    {
        canvasManager = FindObjectOfType<CanvasTool.CanvasManager>();
    }

    protected override void Clicked()
    {
        var sceneModule = bluModule.Application.instance.sceneModule;
        if (next)
        {
            if (bluModule.Application.instance.settingsModule.saveData.GetLevelsComplete() > sceneModule.menuLevel + 5)
                sceneModule.menuLevel += 5;
        }
        else
        {
            if (sceneModule.menuLevel >= 5)
                sceneModule.menuLevel -= 5;
        }

        TextMeshProUGUI[] text = canvasManager.GetCanvasContainer("ButtonText").gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < 5; i++)
        {
            text[i].text = "Level " + (i + sceneModule.menuLevel + 1);
            transform.parent.GetComponentsInChildren<MenuSwap>()[i].SetScene("Level" + (i + sceneModule.menuLevel + 1));
        }

        GetComponentInParent<LevelSelection>().CheckLevels();
    }
}