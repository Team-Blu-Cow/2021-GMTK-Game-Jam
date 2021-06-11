using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public class Application : MonoBehaviour
{
    [HideInInspector] public Module sceneModule = null;

    [HideInInspector] public Module audioModule = null;

    [HideInInspector] public Module settingsModule = null;

    private void Awake()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0: // splash screen
                GameObject.Instantiate(Resources.Load<GameObject>("Scene Module")).transform.parent = transform;
                sceneModule = GetComponentInChildren<SceneModule>();
                break;

            case 1: // main menu
                GameObject.Instantiate(Resources.Load<GameObject>("Audio Module")).transform.parent = transform;
                audioModule = GetComponentInChildren<AudioModule>();
                break;

            default: // gameplay
                GameObject.Instantiate(Resources.Load<GameObject>("Scene Module")).transform.parent = transform;
                sceneModule = GetComponentInChildren<SceneModule>();
                GameObject.Instantiate(Resources.Load<GameObject>("Audio Module")).transform.parent = transform;
                audioModule = GetComponentInChildren<AudioModule>();
                GameObject.Instantiate(Resources.Load<GameObject>("Settings Module")).transform.parent = transform;
                settingsModule = GetComponentInChildren<SettingsModule>();
                break;
        }
    }
}