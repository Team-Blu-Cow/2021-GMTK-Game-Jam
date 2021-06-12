using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class BlockSpawner : Node
    {
        [SerializeField]
        private GameObject m_blockPrefab = null;

        private GameObject m_linkedBlock = null;

        private bool m_wasPowered = false;

        [SerializeField]
        private Transform m_spawnPoint;

        private void FixedUpdate()
        {
            if (IsPowered() && !m_wasPowered)
            {
                if (m_linkedBlock != null)
                {
                    GameObject.Destroy(m_linkedBlock);
                }

                Vector3 pos = m_spawnPoint.position;
                m_linkedBlock = GameObject.Instantiate(m_blockPrefab, pos, Quaternion.identity);
            }

            m_wasPowered = IsPowered();
        }
    }
}