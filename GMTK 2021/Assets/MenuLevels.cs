using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevels : MenuButton
{
    protected override void Clicked()
    {
        if (m_powered)
            Application.Quit();
    }
}