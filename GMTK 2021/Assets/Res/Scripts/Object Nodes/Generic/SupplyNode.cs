using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class SupplyNode : Node
    {
        protected void PowerConnectedNodes()
        {
            Queue<Node> nodes = new Queue<Node>();
            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                Node node = nodes.Dequeue();
                List<NodeConnection> conns = node.Connections;
                for (int i = 0; i < conns.Count; i++)
                {
                    if (conns[i] != null)
                    {
                        if (conns[i].other != null)
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
}