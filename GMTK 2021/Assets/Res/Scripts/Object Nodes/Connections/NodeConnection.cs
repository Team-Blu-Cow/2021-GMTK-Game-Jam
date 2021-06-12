using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    [System.Serializable]
    public class NodeConnection : MonoBehaviour
    {
        private Nodes.NodeConnection m_other = null;

        public Nodes.NodeConnection other { get { return m_other; } }

        protected Nodes.Node m_node = null;

        private void Awake()
        {
            m_node = transform.parent.gameObject.GetComponent<Nodes.Node>();
        }

        public bool HasConnection { get { return m_other != null; } }

        public Nodes.Node node { get { return m_node; } }

        public void SetConnection(Nodes.NodeConnection node)
        {
            m_other = node;
        }

        public void Disconnect()
        {
            if (m_other != null)
            {
                m_other.SetConnection(null);
                this.SetConnection(null);
            }
        }
    }
}