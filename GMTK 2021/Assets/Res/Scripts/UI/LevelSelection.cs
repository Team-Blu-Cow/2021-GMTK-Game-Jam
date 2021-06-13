using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    private Button[] levels;

    // Start is called before the first frame update
    private void Start()
    {
        levels = GetComponentsInChildren<Button>();
        int levelsComplete = bluModule.Application.instance.settingsModule.saveData.GetLevelsComplete();

        if (levelsComplete > levels.Length - 1)
        {
            levelsComplete = levels.Length - 1;
        }

        foreach (Button btn in levels)
        {
            btn.interactable = false;
        }

        levels[levels.Length - 1].interactable = true;

        ColorBlock colors;

        for (int i = 0; i < levelsComplete; i++)
        {
            colors = levels[i].colors;
            colors.normalColor = Color.green;
            levels[i].colors = colors;
            levels[i].interactable = true;

            int copy = i + 1;
            levels[i].onClick.AddListener(delegate { bluModule.Application.instance.sceneModule.currentLevel = copy; });
        }

        colors = levels[levelsComplete].colors;
        colors.normalColor = Color.red;
        levels[levelsComplete].colors = colors;
        levels[levelsComplete].interactable = true;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}