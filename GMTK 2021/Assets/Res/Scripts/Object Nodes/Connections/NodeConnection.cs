using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    [System.Serializable]
    public class NodeConnection : MonoBehaviour
    {
        private Nodes.NodeConnection m_other = null;

        Nodes.Node m_node = null;

        private void Start()
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
            m_other.SetConnection(null);
            this.SetConnection(null);
        }

    }
}