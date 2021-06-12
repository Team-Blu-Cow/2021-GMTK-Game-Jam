using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class CablePlug : Node
    {
        [SerializeField]
        private NodeConnection m_supplyConnection = null;

        [SerializeField]
        private NodeConnection node_in = null;

        [SerializeField]
        private NodeConnection node_out = null;

        protected override void Start()
        {
            node_in.Connect(m_supplyConnection);
            NodeClock.Instance.NodeUpdate += OnInvoke;
        }

        private void OnDrawGizmos()
        {
            if (m_supplyConnection != null)
            {
                if (Connections.Count > 0)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(m_supplyConnection.transform.position, Connections[0].transform.position);
                }
            }
        }
    }
}