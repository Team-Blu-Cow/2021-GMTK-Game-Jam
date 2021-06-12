using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class Gate_AND : LogicGate
    {
        public Gate_AND()
        {
            m_type = LogicGateType.AND;
        }

        public override bool IsGatesOutputsActive()
        {
            bool ret = true;

            for (int i = 0; i < m_gateStates.Length; i++)
            {
                if (m_gateStates[i] == false)
                {
                    ret = false;
                    break;
                }
            }

            return ret;
        }
    }
}