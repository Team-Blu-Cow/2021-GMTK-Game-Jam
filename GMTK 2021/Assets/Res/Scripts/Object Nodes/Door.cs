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

        protected override void Awake()
        {
            NodeConnection[] Con = gameObject.transform.GetComponentsInChildren<NodeConnection>();
            for (int i = 0; i < Con.Length; i++)
            {
                m_Connections.Add(Con[i]);
            }

            boxcollider = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            if (IsPowered())
            {
                if (!m_hasRunBefore && m_wasPowered == false)
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/interactables/door/open");

                boxcollider.enabled = false;
                m_wasPowered = true;
            }
            else
            {
                if (!m_hasRunBefore && m_wasPowered == true)
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/interactables/door/close");

                boxcollider.enabled = true;
                m_wasPowered = false;
            }

            m_hasRunBefore = true;
        }
    }
}