using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes

{
    public class Gate_NAND : LogicGate
    {
        public Gate_NAND()
        {
            m_type = LogicGateType.NAND;
        }

        public override bool IsGatesOutputsActive()
        {
            bool ret = false;

            for (int i = 0; i < m_gateStates.Length; i++)
            {
                if (m_gateStates[i] == false)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }
    }
}