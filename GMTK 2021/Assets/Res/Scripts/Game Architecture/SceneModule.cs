using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace bluModule
{
    public class SceneModule : Module
    {
        private Animator transition;
        private GameObject transitionGO;
        public int currentLevel = 0;

        public void SwitchScene(string in_Scene)
        {
            StartCoroutine(LoadLevel(in_Scene));
            bluModule.Application.instance.audioModule.StopAllEvents(true);
        }

        public void Quit()
        {
            Debug.Log("Application Quitting");
            UnityEngine.Application.Quit();
        }

        private IEnumerator LoadLevel(string in_Scene)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(in_Scene);
            transition.SetTrigger("End");
        }

        private void Awake()
        {
            transitionGO = Instantiate(Resources.Load<GameObject>("Animations/Transition"), transform);
            transitionGO.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            transitionGO.GetComponent<Canvas>().worldCamera = Camera.main; //!! camera Main, store camera in app class

            transition = transitionGO.GetComponent<Animator>();
        }
    }
}