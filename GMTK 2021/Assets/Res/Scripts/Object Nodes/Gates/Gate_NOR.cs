using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class Gate_NOR : LogicGate
    {
        public Gate_NOR()
        {
            m_type = LogicGateType.NOR;
        }

        public override bool IsGatesOutputsActive()
        {
            for (int i = 0; i < m_gateStates.Length; i++)
            {
                if (m_gateStates[i] == true)
                    return false;
            }

            return true;
        }
    }
}