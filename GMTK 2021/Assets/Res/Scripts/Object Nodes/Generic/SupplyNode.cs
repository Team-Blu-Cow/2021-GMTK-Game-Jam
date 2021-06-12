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
                    if (conns[i] != null && conns[i].other != null && conns[i].other.node != null)
                    {
                        if (conns[i].other.node.IsPowered())
                        { continue; }

                        if (conns[i].other.node.isLogicGate)
                        {
                            ProcessGate(conns[i], nodes);
                        }
                        else
                        {
                            conns[i].other.node.SetPowered();
                            nodes.Enqueue(conns[i].other.node);
                        }
                    }
                }
            }
        }

        private void ProcessGate(NodeConnection connection, Queue<Node> nodes)
        {
            LogicGate gate = (LogicGate)connection.other.node;
            gate.ActivateConnection(connection.other);

            if (gate.IsGatesOutputsActive())
            {
                List<NodeConnection> gateConns = gate.GateOutputs;

                for (int c = 0; c < gateConns.Count; c++)
                {
                    if (!gateConns[c].other.node.IsPowered())
                    {
                        nodes.Enqueue(gateConns[c].other.node);
                    }
                }

                if (!connection.other.node.IsPowered())
                {
                    connection.other.node.SetPowered();
                    nodes.Enqueue(connection.other.node);
                }
            }
        }
    }
}