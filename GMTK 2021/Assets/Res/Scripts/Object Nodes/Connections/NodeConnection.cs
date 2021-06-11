using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    [System.Serializable]
    public class NodeConnection : MonoBehaviour
    {
        private Nodes.Node m_other = null;

        public bool HasConnection { get { return m_other != null; } }

        public Nodes.Node node { get { return m_other; } }

        public bool Connect(Nodes.Node node)
        {
            if (HasConnection)
            {
                return false;
            }
            else
            {
                m_other = node;
                return true;
            }
        }
    }
}