using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NodeClock : MonoBehaviour
{
    private static NodeClock m_instance = null;

    private float clockAccumulator = 0f;

    public static NodeClock Instance { get { return m_instance; } }

    public event Action NodeUpdate;

    public event Action NodePowerSupplyUpdate;

    public event Action NodeLogicGates;

    private void OnEnable()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public List<Nodes.NodeConnection> m_Connectors = new List<Nodes.NodeConnection>();

    private void Update()
    {
        clockAccumulator += Time.deltaTime;

        if (clockAccumulator > 0.1f)
        {
            NodeUpdate?.Invoke();
            NodePowerSupplyUpdate?.Invoke();
            NodeLogicGates?.Invoke();
        }
    }
}