using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwap : MenuButton
{
    public string m_SceneSwap;

    protected override void Clicked()
    {
        if (m_SceneSwap.Contains("Level"))
        {
            bluModule.Application.instance.sceneModule.currentLevel = m_SceneSwap[5] - 48;
        }
        bluModule.Application.instance.sceneModule.SwitchScene(m_SceneSwap);
    }

    public void SetScene(string scene)
    {
        m_SceneSwap = scene;
    }
}