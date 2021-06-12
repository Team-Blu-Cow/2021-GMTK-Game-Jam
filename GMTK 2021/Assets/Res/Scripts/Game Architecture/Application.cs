using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace bluModule
{
    public class Application : MonoBehaviour
    {
        [HideInInspector] public static Application instance = null;

        [HideInInspector] public bluModule.SceneModule sceneModule = null;

        [HideInInspector] public bluModule.AudioModule audioModule = null;

        [HideInInspector] public bluModule.SettingsModule settingsModule = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0: // splash screen
                    GameObject.Instantiate(Resources.Load<GameObject>("Settings Module")).transform.parent = transform;
                    settingsModule = GetComponentInChildren<bluModule.SettingsModule>();
                    GameObject.Instantiate(Resources.Load<GameObject>("Scene Module")).transform.parent = transform;
                    sceneModule = GetComponentInChildren<bluModule.SceneModule>();
                    GameObject.Instantiate(Resources.Load<GameObject>("Audio Module")).transform.parent = transform;
                    audioModule = GetComponentInChildren<bluModule.AudioModule>();
                    audioModule.Init();
                    break;

                case 1: // main menu
                    GameObject.Instantiate(Resources.Load<GameObject>("Settings Module")).transform.parent = transform;
                    settingsModule = GetComponentInChildren<bluModule.SettingsModule>();
                    GameObject.Instantiate(Resources.Load<GameObject>("Audio Module")).transform.parent = transform;
                    audioModule = GetComponentInChildren<bluModule.AudioModule>();
                    audioModule.Init();

                    break;

                default: // gameplay
                    GameObject.Instantiate(Resources.Load<GameObject>("Settings Module")).transform.parent = transform;
                    settingsModule = GetComponentInChildren<bluModule.SettingsModule>();
                    GameObject.Instantiate(Resources.Load<GameObject>("Scene Module")).transform.parent = transform;
                    sceneModule = GetComponentInChildren<bluModule.SceneModule>();
                    GameObject.Instantiate(Resources.Load<GameObject>("Audio Module")).transform.parent = transform;
                    audioModule = GetComponentInChildren<bluModule.AudioModule>();
                    audioModule.Init();
                    break;
            }
        }
    }
}