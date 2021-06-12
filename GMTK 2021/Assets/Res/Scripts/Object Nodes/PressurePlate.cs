using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class PressurePlate : SupplyNode
    {
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
            if (m_isPowered && position.y > 0.35)
            {
                transform.parent.localPosition = new Vector3(position.x, position.y - 0.005f, position.z);
            }
            else if (position.y < 0.65)
            {
                transform.parent.localPosition = new Vector3(position.x, position.y + 0.005f, position.z);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Box")
            {
                m_isPowered = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Box")
            {
                m_isPowered = false;
            }
        }

        protected override void OnInvoke()
        {
            if (m_isPowered)
            {
                PowerConnectedNodes();
            }
        }
    }
}