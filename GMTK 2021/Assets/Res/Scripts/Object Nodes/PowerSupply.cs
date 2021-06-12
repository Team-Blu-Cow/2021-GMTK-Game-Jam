using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class PowerSupply : Node
    {
        protected override void Start()
        {
            NodeClock.Instance.NodePowerSupplyUpdate += OnInvoke;
        }

        protected override void OnDestroy()
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
                        if(conns[i].other != null)
                        {

                            if (conns[i].other.node != null)
                            {
                                if (!conns[i].other.node.IsPowered())
                                {
                                    conns[i].other.node.SetPowered();
                                    nodes.Enqueue(conns[i].other.node);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
};