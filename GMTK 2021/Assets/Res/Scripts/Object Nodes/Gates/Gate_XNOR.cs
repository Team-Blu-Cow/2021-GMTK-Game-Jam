using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class Gate_XNOR : LogicGate
    {
        public Gate_XNOR()
        {
            m_type = LogicGateType.XNOR;
        }

        public override bool IsGatesOutputsActive()
        {
            int count_true = 0;

            for (int i = 0; i < m_gateStates.Length; i++)
            {
                if (m_gateStates[i] == true)
                    count_true++;
            }

            if (count_true == 0 || count_true == m_gateStates.Length)
                return true;

            return false;
        }
    }
}