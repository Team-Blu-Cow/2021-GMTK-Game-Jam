using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectDropNoises
{
    public static IEnumerator DropObjectSound(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);

        bool looping = true;

        Rigidbody2D body = obj.GetComponent<Rigidbody2D>();

        if (body == null)
            looping = false;

        while (looping)
        {
            if (Mathf.Abs(body.velocity.y) < 0.01)
            {
                bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/interactables/plugs/put down");
                looping = false;
                yield break;
            }

            yield return null;
        }
    }
}