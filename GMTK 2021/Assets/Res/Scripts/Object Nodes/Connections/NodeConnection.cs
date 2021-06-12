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

        private void Start()
        {
            NodeClock.Instance.m_Connectors.Add(this);
        }

        private void OnDestroy()
        {
            NodeClock.Instance.m_Connectors.Remove(this);
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

        public bool Connect(NodeConnection conn)
        {
            if (conn == null)
                return false;

            GameObject connParent = conn.transform.parent.gameObject;
            GameObject thisParent = transform.parent.gameObject;

            if (connParent == null)
                return false;

            if (connParent == thisParent)
                return false;

            if (conn.HasConnection)
                return false;

            if (CheckForCircularConnection(conn))
                return false;

            conn.SetConnection(this);
            SetConnection(conn);

            return true;
        }

        // return true if there is an error
        private bool CheckForCircularConnection(NodeConnection conn)
        {
            Queue<GameObject> toVisit = new Queue<GameObject>();

            toVisit.Enqueue(conn.transform.parent.gameObject);
            while (toVisit.Count > 0)
            {
                GameObject current = toVisit.Dequeue();

                if (current == this)
                    return true;

                NodeConnection[] conns = current.GetComponents<NodeConnection>();
                for (int i = 0; i < conns.Length; i++)
                {
                    toVisit.Enqueue(conns[i].gameObject);
                }
            }

            return false;
        }
    }
}