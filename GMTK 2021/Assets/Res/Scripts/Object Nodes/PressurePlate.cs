using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class PressurePlate : SupplyNode
    {
        private int m_collisionCount;

        private bool m_isPressed = false;

        protected override void Start()
        {
            NodeClock.Instance.NodePowerSupplyUpdate += OnInvoke;
        }

        protected override void OnDestroy()
        {
            NodeClock.Instance.NodePowerSupplyUpdate -= OnInvoke;
        }

        // Update is called once per frame
        private void Update()
        {
            Vector3 position = transform.parent.localPosition;
            if (m_isPressed && position.y > -0.07)
            {
                transform.parent.localPosition = new Vector3(position.x, position.y - 0.005f, position.z);
            }
            else if (!m_isPressed && position.y < 0)
            {
                transform.parent.localPosition = new Vector3(position.x, position.y + 0.005f, position.z);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Pickup") || collision.CompareTag("Player"))
            {
                if (m_collisionCount == 0)
                {
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/interactables/pressure plates/insert");
                }

                m_collisionCount++;
                m_isPressed = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Pickup") || collision.CompareTag("Player"))
            {
                m_collisionCount--;
                if (m_collisionCount == 0)
                {
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/interactables/pressure plates/remove");
                    m_isPressed = false;
                }
            }
        }

        protected override void OnInvoke()
        {
            m_isPowered = false;
            if (m_isPressed)
            {
                PowerConnectedNodes();
            }
        }
    }
}