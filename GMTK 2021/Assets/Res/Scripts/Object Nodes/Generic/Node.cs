using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public abstract class Node : MonoBehaviour
    {
        protected bool m_isPowered = false;

        public void SetPowered()
        {
            m_isPowered = true;
        }

        private void Awake()
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