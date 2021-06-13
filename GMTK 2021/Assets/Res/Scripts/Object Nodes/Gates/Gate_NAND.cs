using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes

{
    public class Gate_NAND : LogicGate
    {
        public Gate_NAND()
        {
            m_type = LogicGateType.NAND;
        }

        public override bool IsGatesOutputsActive()
        {
            bool ret = false;

            for (int i = 0; i < m_gateStates.Length; i++)
            {
                if (m_gateStates[i] == false)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        public override void OnLogicInvoke()
        {
            if ((m_gateStates[0] && m_gateStates[1]) == false)
            {
                m_isPowered = true;

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
                            if (conns[i].other.node.isLogicGate)
                            {
                                LogicGate gate = (LogicGate)conns[i].other.node;
                                gate.ActivateConnection(conns[i].other);

                                if (gate.IsGatesOutputsActive())
                                {
                                    List<NodeConnection> gateConns = gate.GateOutputs;

                                    for (int c = 0; c < gateConns.Count; c++)
                                    {
                                        if (!gateConns[c].other.node.IsPowered())
                                        {
                                            conns[i].other.node.SetPowered();
                                            nodes.Enqueue(gateConns[c].other.node);
                                        }
                                    }

                                    if (!conns[i].other.node.IsPowered())
                                    {
                                        conns[i].other.node.SetPowered();
                                        nodes.Enqueue(conns[i].other.node);
                                    }
                                }
                            }
                            else
                            {
                                if (conns[i].other.node.IsPowered())
                                { continue; }

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