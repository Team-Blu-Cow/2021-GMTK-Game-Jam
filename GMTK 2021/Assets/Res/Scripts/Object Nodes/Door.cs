using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class Door : Node
    {
        private bool m_wasPowered = false;
        private bool m_hasRunBefore = false;

        private BoxCollider2D boxcollider;

        private Animator anim;

        protected override void Awake()
        {
            NodeConnection[] Con = gameObject.transform.GetComponentsInChildren<NodeConnection>();
            for (int i = 0; i < Con.Length; i++)
            {
                m_Connections.Add(Con[i]);
            }

            boxcollider = GetComponent<BoxCollider2D>();
            anim        = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {

            if (IsPowered())
            {
                if (m_wasPowered == false)
                {
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/interactables/door/open");
                    anim.SetBool("open", true);
                }

                boxcollider.enabled = false;
                m_wasPowered = true;
            }
            else
            {
                if (m_wasPowered == true)
                {
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/interactables/door/close");
                    anim.SetBool("open", false);
                }

                boxcollider.enabled = true;
                m_wasPowered = false;
            }

            m_hasRunBefore = true;
        }
    }
}