using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class PressurePlate : Node
    {
        public bool powered = false;

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            Vector3 position = transform.parent.localPosition;
            if (powered && position.y > 0.35)
            {
                transform.parent.localPosition = new Vector3(position.x, position.y - 0.005f, position.z);
            }
            else if (position.y < 0.65)
            {
                transform.parent.localPosition = new Vector3(position.x, position.y + 0.005f, position.z);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Box")
            {
                powered = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Box")
            {
                powered = false;
            }
        }

        public override bool IsSupplyingPower()
        {
            return powered;
        }
    }
}