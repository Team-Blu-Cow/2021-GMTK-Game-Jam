using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public enum LogicGateType
    {
        _notype,
        AND,
        OR,
        NOT,
        NAND,
        NOR,
        XOR
    }

    public abstract class LogicGate : Node
    {
        [SerializeField]
        private List<NodeConnection> m_inputConnections = new List<NodeConnection>();

        [SerializeField]
        private List<NodeConnection> m_outputConnections = new List<NodeConnection>();

        protected LogicGateType m_type = LogicGateType._notype;

        public LogicGateType gateType { get { return m_type; } }

        protected bool[] m_gateStates = null;

        public LogicGate()
        {
            m_isLogicGate = true;
            m_gateStates = new bool[m_inputConnections.Count];
            DisableInputs();
        }

        protected override void Awake()
        {
            m_gateStates = new bool[m_inputConnections.Count];
            DisableInputs();
        }

        public abstract bool IsGatesOutputsActive();

        public List<NodeConnection> GateOutputs { get { return m_outputConnections; } }

        public void ActivateConnection(NodeConnection conn)
        {
            for (int i = 0; i < m_inputConnections.Count; i++)
            {
                if (conn == m_inputConnections[i])
                {
                    m_gateStates[i] = true;
                }
            }
        }

        private void DisableInputs()
        {
            for (int i = 0; i < m_gateStates.Length; i++)
            { m_gateStates[i] = false; }
        }
    }
}