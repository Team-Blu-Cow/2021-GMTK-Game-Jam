using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public abstract class Node : MonoBehaviour
    {
        private void Awake()
        {
            InputConnection[] inCon = gameObject.transform.GetComponentsInChildren<InputConnection>();
            OutputConnection[] outCon = gameObject.transform.GetComponentsInChildren<OutputConnection>();

            for (int i = 0; i < inCon.Length; i++)
            {
                m_inputConnections.Add(inCon[i]);
            }

            for (int i = 0; i < outCon.Length; i++)
            {
                m_outputConnections.Add(outCon[i]);
            }
        }

        protected List<InputConnection> m_inputConnections = new List<InputConnection>();

        protected List<OutputConnection> m_outputConnections = new List<OutputConnection>();

        public abstract bool IsSupplyingPower();

        public bool IsPowered()
        {
            if (IsSupplyingPower())
            { return true; }

            for (int i = 0; i < m_inputConnections.Count; i++)
            {
                if (m_inputConnections[i] != null)
                {
                    if (m_inputConnections[i].node != null)
                    {
                        if (m_inputConnections[i].node.IsPowered())
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
};