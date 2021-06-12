using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwap : MenuButton
{
    [SerializeField]
    private string m_SceneSwap;

    protected override void Clicked()
    {
        if (m_powered)
            bluModule.Application.instance.sceneModule.SwitchScene(m_SceneSwap);
    }
}