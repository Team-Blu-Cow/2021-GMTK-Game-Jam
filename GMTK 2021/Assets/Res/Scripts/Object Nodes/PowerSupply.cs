using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class PowerSupply : Node
    {
        protected override void OnEnable()
        {
            NodeClock.Instance.NodePowerSupplyUpdate += OnInvoke;
        }

        protected override void OnDisable()
        {
            NodeClock.Instance.NodePowerSupplyUpdate -= OnInvoke;
        }

        protected override void OnInvoke()
        {
            m_isPowered = true;

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
};