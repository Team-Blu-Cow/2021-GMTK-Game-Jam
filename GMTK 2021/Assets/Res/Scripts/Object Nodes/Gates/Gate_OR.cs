using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class Gate_OR : LogicGate
    {
        public Gate_OR()
        {
            m_type = LogicGateType.OR;
        }

        public override bool IsGatesOutputsActive()
        {
            for (int i = 0; i < m_gateStates.Length; i++)
            {
                if (m_gateStates[i] == true)
                    return true;
            }

            return false;
        }
    }
}