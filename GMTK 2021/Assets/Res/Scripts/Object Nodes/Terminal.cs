using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class Terminal : Node
    {
        [SerializeField]
        private NodeConnection m_connectionSocket = null;

        [SerializeField]
        private NodeConnection m_connectionWire = null;

        [SerializeField]
        private NodeConnection m_connectionOther = null;

        protected override void Start()
        {
            if (m_connectionWire != null && m_connectionOther != null)
            {
                m_connectionWire.Connect(m_connectionOther, false);
            }

            NodeClock.Instance.NodeUpdate += OnInvoke;
        }

        // private void OnDrawGizmos()
        // {
        //     if (m_connectionWire != null && m_connectionOther != null)
        //     {
        //         if (m_connectionWire.other == m_connectionOther)
        //         {
        //             Gizmos.color = Color.red;
        //             Gizmos.DrawLine(m_connectionWire.transform.position, m_connectionOther.transform.position);
        //         }
        //     }
        // }
    }
}