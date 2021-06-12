using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator bodyAnim;
    Animator armsAnim;
    bool flipped = false;
    bool falling = false;

    [SerializeField] Transform mainAnimator;
    [SerializeField] Transform body;
    [SerializeField] Transform arms;

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
        bodyAnim.SetBool(name, state);
        armsAnim.SetBool(name, state);
    }

    public void UpdateAnim(Vector3 velocity)
    {
        if (velocity.x != 0)
        {
            mainAnimator.localScale = new Vector3(Mathf.Sign(velocity.x), 1, 1);
        }

        SetBool("isFalling", (velocity.y <= 0)? true : false);
    }

    public static void Footstep()
    {
        bluModule.Application.instance.audioModule.NewOneShot("event:/player/footstep");
    }


}
