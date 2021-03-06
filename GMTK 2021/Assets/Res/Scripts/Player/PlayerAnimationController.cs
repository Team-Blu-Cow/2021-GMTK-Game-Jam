using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator bodyAnim;
    private Animator armsAnim;
    private bool flipped = false;
    private bool falling = false;

    [SerializeField] private Transform mainAnimator;
    [SerializeField] private Transform body;
    [SerializeField] private Transform arms;

    public void Start()
    {
        mainAnimator = transform.Find("Animations");
        body = mainAnimator.Find("Body");
        arms = body.Find("Arms");

        bodyAnim = body.GetComponent<Animator>();
        armsAnim = arms.GetComponent<Animator>();

        //Time.timeScale = 0.25f;
    }

    public void SetBool(string name, bool state)
    {
        if (name != "isHoldingCable" || name != "isHolding")
            bodyAnim.SetBool(name, state);
        armsAnim.SetBool(name, state);
    }

    public void UpdateAnim(Vector3 velocity)
    {
        if (Mathf.Abs(velocity.x) >= PlayerMovement.directionEpsilon)
        {
            mainAnimator.localScale = new Vector3(Mathf.Sign(velocity.x), 1, 1);
        }

        falling = (velocity.y <= 0) ? true : false;

        SetBool("isFalling", falling);
        if(falling)
            SetBool("isJumping", false);
    }

    public static void Footstep()
    {
        bluModule.Application.instance.audioModule.PlayAudioEvent("event:/player/footstep");
    }
}