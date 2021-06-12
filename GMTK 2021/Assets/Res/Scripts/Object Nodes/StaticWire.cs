using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class StaticWire : Node
    {
        [SerializeField]
        private NodeConnection m_connectionOne = null;

        [SerializeField]
        private NodeConnection m_connectionTwo = null;

        protected override void Start()
        {
            if (m_connectionOne != null && m_connectionTwo != null)
            {
                m_connectionOne.Connect(m_connectionTwo, false);
            }

            NodeClock.Instance.NodeUpdate += OnInvoke;
        }

        // private void OnDrawGizmos()
        // {
        //     if (m_connectionOne != null && m_connectionTwo != null)
        //     {
        //         if (m_connectionOne.other == m_connectionTwo)
        //         {
        //             Gizmos.color = Color.red;
        //             Gizmos.DrawLine(m_connectionOne.transform.position, m_connectionTwo.transform.position);
        //         }
        //     }
        // }
    }
}