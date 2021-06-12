using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class CablePlug : Node
    {
        [SerializeField]
        private OutputConnection m_supplyConnection = null;

        protected override void Start()
        {
            InputConnection incon = GetComponentInChildren<InputConnection>();
            incon.Connect(m_supplyConnection);
            NodeClock.Instance.NodeUpdate += OnInvoke;
        }

        private void OnDrawGizmos()
        {
            if (m_supplyConnection != null)
            {
                if (inputConnections.Count > 0)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(m_supplyConnection.transform.position, inputConnections[0].transform.position);
                }
            }
        }
    }
}