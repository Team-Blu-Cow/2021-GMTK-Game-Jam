using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class Door : Node
    {
        private BoxCollider2D boxcollider;

        private void Awake()
        {
            boxcollider = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            if (IsPowered())
            {
                boxcollider.enabled = false;
            }
            else
            {
                boxcollider.enabled = true;
            }
        }
    }
}