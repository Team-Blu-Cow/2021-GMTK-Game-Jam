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
        XOR,
        XNOR
    }

    public abstract class LogicGate : Node
    {
        [SerializeField]
        protected List<NodeConnection> m_inputConnections = new List<NodeConnection>();

        [SerializeField]
        protected List<NodeConnection> m_outputConnections = new List<NodeConnection>();

        protected LogicGateType m_type = LogicGateType._notype;

        public LogicGateType gateType { get { return m_type; } }

        [SerializeField] protected Sprite[] m_symbolSprites;

        [SerializeField] protected bool[] m_gateStates = null;

        [SerializeField] protected SpriteRenderer m_symbolRenderer;

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
            m_symbolRenderer = transform.Find("power symbol").GetComponent<SpriteRenderer>();
        }

        protected override void Start()
        {
            NodeClock.Instance.NodeUpdate += OnInvoke;
            NodeClock.Instance.NodeLogicGates += OnLogicInvoke;
        }

        protected override void OnDestroy()
        {
            NodeClock.Instance.NodeUpdate -= OnInvoke;
            NodeClock.Instance.NodeLogicGates -= OnLogicInvoke;
        }

        public virtual void OnLogicInvoke() { }

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

        protected override void OnInvoke()
        {
            m_isPowered = false;

            for (int i = 0; i < m_gateStates.Length; i++)
            {
                m_gateStates[i] = false;
            }
        }

        private void DisableInputs()
        {
            for (int i = 0; i < m_gateStates.Length; i++)
            { m_gateStates[i] = false; }
        }

        private void Update()
        {
            if (m_symbolSprites == null || m_symbolSprites.Length != 4)
                return;

            if(m_gateStates[0])
            {
                if(m_gateStates[1])
                    m_symbolRenderer.sprite = m_symbolSprites[0];
                else
                    m_symbolRenderer.sprite = m_symbolSprites[2];
            }
            else
            {
                if (m_gateStates[1])
                    m_symbolRenderer.sprite = m_symbolSprites[3];
                else
                    m_symbolRenderer.sprite = m_symbolSprites[1];
            }
        }

    }
}