using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nodes
{
    public class GravityPad : Node
    {
        [SerializeField]
        private float m_force;

        public ParticleSystem particles;

        private bool wasPowered;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsPowered())
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(new Vector2(0, m_force), ForceMode2D.Impulse);
                }
                //particles.Play();
            }
        }

        private void Update()
        {
            if (IsPowered() && !wasPowered)
                particles.Play();

            if (!IsPowered())
                particles.Stop();

            wasPowered = IsPowered();
        }
    }
}