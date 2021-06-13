using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuQuit : MenuButton
{
    protected override void Clicked()
    {
        Application.Quit();
    }
}