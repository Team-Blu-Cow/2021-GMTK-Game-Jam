using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class Node : MonoBehaviour
    {
        protected bool m_isPowered = false;

        protected bool m_isLogicGate = false;

        public bool isLogicGate { get { return m_isLogicGate; } }

        public void SetPowered(bool powered = true)
        {
            m_isPowered = powered;
        }

        protected virtual void Awake()
        {
            NodeConnection[] Con = gameObject.transform.GetComponentsInChildren<NodeConnection>();

            for (int i = 0; i < Con.Length; i++)
            {
                m_Connections.Add(Con[i]);
            }
        }

        protected virtual void Start()
        {
            NodeClock.Instance.NodeUpdate += OnInvoke;
        }

        protected virtual void OnDestroy()
        {
            NodeClock.Instance.NodeUpdate -= OnInvoke;
        }

        protected virtual void OnInvoke()
        {
            m_isPowered = false;
        }

        protected List<NodeConnection> m_Connections = new List<NodeConnection>();

        public List<NodeConnection> Connections { get { return m_Connections; } }

        public bool IsPowered()
        {
            return m_isPowered;
        }
    }
};