using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TunesMachine : MonoBehaviour
{
    private float ratio = 0f;

    // Start is called before the first frame update
    private bool track = false;

    private bool done = true;

    private void Start()
    {
        bluModule.Application.instance.audioModule.PlayMusicEvent("event:/music/Main theme");
    }

    public void SwapTrack()
    {
        if (done)
        {
            done = false;
            StartCoroutine(lerpTunes(track));
        }
    }

    // Update is called once per frame
    private IEnumerator lerpTunes(bool in_track)
    {
        float currentTime = 0;
        if (in_track) // lerp to 1
        {
            while (currentTime < 1f)
            {
                currentTime += Time.deltaTime;
                ratio = Mathf.Lerp(0, 1, currentTime / 1f);
                yield return null;
            }
        }
        else // lerp to 0
        {
            while (currentTime < 1f)
            {
                currentTime += Time.deltaTime;
                ratio = Mathf.Lerp(1, 0, currentTime / 1f);
                yield return null;
            }
        }

        done = true;
    }
}