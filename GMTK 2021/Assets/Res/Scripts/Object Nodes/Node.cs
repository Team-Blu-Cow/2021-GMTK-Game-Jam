using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public abstract class Node : MonoBehaviour
    {
        protected bool m_isPowered = false;

        public void SetPowered()
        {
            m_isPowered = true;
        }

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

        protected virtual void OnEnable()
        {
            NodeClock.Instance.NodeUpdate += OnInvoke;
        }

        protected virtual void OnDisable()
        {
            NodeClock.Instance.NodeUpdate -= OnInvoke;
        }

        protected virtual void OnInvoke()
        {
            m_isPowered = false;
        }

        protected List<InputConnection> m_inputConnections = new List<InputConnection>();

        protected List<OutputConnection> m_outputConnections = new List<OutputConnection>();

        public List<InputConnection> inputConnections { get { return m_inputConnections; } }
        public List<OutputConnection> outputConnections { get { return m_outputConnections; } }

        public bool IsPowered()
        {
            return m_isPowered;
        }
    }
};