using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    private MenuSwap[] m_levels;

    // Start is called before the first frame update
    private void Start()
    {
        CheckLevels();
    }

    public void CheckLevels()
    {
        m_levels = GetComponentsInChildren<MenuSwap>();

        int levelsComplete = bluModule.Application.instance.settingsModule.saveData.GetLevelsComplete();

        if (levelsComplete > bluModule.Application.instance.sceneModule.MAX_LEVELS)
        {
            levelsComplete = bluModule.Application.instance.sceneModule.MAX_LEVELS;
        }

        if (levelsComplete < bluModule.Application.instance.sceneModule.menuLevel + 5)
        {
            if (levelsComplete != 0)
            {
                levelsComplete %= 5;
                if (levelsComplete == 0)
                    levelsComplete = 5;
            }
        }
        else
        {
            levelsComplete = 5;
        }
        

        foreach (MenuSwap btn in m_levels)
        {
            btn.m_active = false;
            //btn.GetComponent<SpriteRenderer>().color = Color.gray;
            btn.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }

        m_levels[m_levels.Length - 1].m_active = true;
        m_levels[m_levels.Length - 1].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;

        for (int i = 0; i < levelsComplete; i++)
        {
            m_levels[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            m_levels[i].m_active = true;
        }

        m_levels[levelsComplete].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        m_levels[levelsComplete].m_active = true;

    }
}