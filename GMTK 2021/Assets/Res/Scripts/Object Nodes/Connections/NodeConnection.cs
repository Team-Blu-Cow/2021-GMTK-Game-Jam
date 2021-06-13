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

        [SerializeField]
        protected Nodes.Node m_node = null;

        public bool isPowered()
        {
            return m_node.IsPowered();
        }

        [SerializeField]
        private bool m_allowPlayerInteraction = true;

        public bool allowPlayerInteraction { get { return m_allowPlayerInteraction; } }

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

        public void Disconnect(bool playAudio = true)
        {
            if (m_other != null)
            {
                if (playAudio)
                    bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/nodes/button remove");

                m_other.SetConnection(null);
                this.SetConnection(null);
            }
        }

        public bool Connect(NodeConnection conn, bool playAudio = true)
        {
            if (conn == null)
                return false;

            GameObject connParent = conn.transform.parent.gameObject;
            GameObject thisParent = transform.parent.gameObject;

            if (connParent == null)
                return false;

            if (connParent == thisParent)
                return false;

            if (conn.HasConnection || this.HasConnection)
                return false;

            if (CheckForCircularConnection(conn))
                return false;

            conn.SetConnection(this);
            SetConnection(conn);

            if (playAudio)
                bluModule.Application.instance.audioModule.PlayAudioEvent("event:/environment/objects/nodes/button insert");

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

        private void OnDrawGizmos()
        {
            if (other != null)
            {
                if (other.other == this)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position, other.transform.position);
                }
            }
        }
    }
}