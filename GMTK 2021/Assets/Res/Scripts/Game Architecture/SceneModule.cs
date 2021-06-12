using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace bluModule
{
    public class SceneModule : Module
    {
        private Animator transition;

        public void SwitchScene(string in_Scene)
        {
            StartCoroutine(LoadLevel(in_Scene));
        }

        private IEnumerator LoadLevel(string in_Scene)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(in_Scene);
        }

        private void Awake()
        {
            transition = Resources.Load<Animator>("Animations/Crossfade");
        }
    }
}