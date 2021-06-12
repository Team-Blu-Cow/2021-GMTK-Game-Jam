using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(sequence());
    }

    // Update is called once per frame
    private IEnumerator sequence()
    {
        yield return new WaitForSeconds(3.5f);
        bluModule.Application.instance.sceneModule.SwitchScene("MainMenu");
    }
}