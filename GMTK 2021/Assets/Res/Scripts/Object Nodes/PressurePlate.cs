using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class PressurePlate : Node
    {
        private int m_collisionCount;

        protected override void OnEnable()
        {
            NodeClock.Instance.NodePowerSupplyUpdate += OnInvoke;
        }

        protected override void OnDisable()
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
            if (collision.CompareTag("Pickup") || collision.CompareTag("Player"))
            {
                m_collisionCount++;
                m_isPowered = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Pickup") || collision.CompareTag("Player"))
            {
                m_collisionCount--;
                if (m_collisionCount == 0)
                {
                    m_isPowered = false;
                }
            }
        }

        protected override void OnInvoke()
        {
            if (m_isPowered)
            {
                Queue<Node> nodes = new Queue<Node>();
                nodes.Enqueue(this);

                while (nodes.Count > 0)
                {
                    Node node = nodes.Dequeue();
                    List<OutputConnection> conns = node.outputConnections;
                    for (int i = 0; i < conns.Count; i++)
                    {
                        if (conns[i] != null)
                        {
                            if (conns[i].node != null)
                            {
                                if (!conns[i].node.IsPowered())
                                {
                                    conns[i].node.SetPowered();
                                    nodes.Enqueue(conns[i].node);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}