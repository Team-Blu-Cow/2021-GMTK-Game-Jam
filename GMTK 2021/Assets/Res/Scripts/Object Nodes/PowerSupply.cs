using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class PowerSupply : SupplyNode
    {
        protected override void Start()
        {
            NodeClock.Instance.NodePowerSupplyUpdate += OnInvoke;
        }

        protected override void OnDestroy()
        {
            NodeClock.Instance.NodePowerSupplyUpdate -= OnInvoke;
        }

        protected override void OnInvoke()
        {
            m_isPowered = true;

            PowerConnectedNodes();
        }
    }
};