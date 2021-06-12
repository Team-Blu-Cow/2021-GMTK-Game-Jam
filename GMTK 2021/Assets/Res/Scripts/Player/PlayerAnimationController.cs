using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator bodyAnim;
    private bool flipped = false;
    private bool falling = false;

    [SerializeField] private Transform mainAnimator;
    [SerializeField] private Transform body;
    [SerializeField] private Transform arms;

    public void Start()
    {
        mainAnimator = transform.Find("Animations");
        body = mainAnimator.Find("Body");

        bodyAnim = body.GetComponent<Animator>();
    }

    public void SetBool(string name, bool state)
    {
        bodyAnim.SetBool(name, state);
    }

    public void UpdateAnim(Vector3 velocity)
    {
        if (velocity.x != 0)
        {
            mainAnimator.localScale = new Vector3(Mathf.Sign(velocity.x), 1, 1);
        }

        bodyAnim.SetBool("isFalling", (velocity.y < 0));
    }

    public static void Footstep()
    {
        bluModule.Application.instance.audioModule.PlayAudioEvent("event:/player/footstep");
    }
}