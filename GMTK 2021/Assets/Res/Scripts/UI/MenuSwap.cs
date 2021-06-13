using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwap : MenuButton
{
    public string m_SceneSwap;

    protected override void Clicked()
    {
            bluModule.Application.instance.sceneModule.SwitchScene(m_SceneSwap);
    }

    public void SetScene(string scene)
    {
        m_SceneSwap = scene;
    }
}