using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class PowerSupply : Node
    {
        public override bool IsSupplyingPower()
        {
            return true;
        }
    }
};