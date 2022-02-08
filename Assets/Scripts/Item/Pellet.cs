using UnityEngine;

namespace Tonhex
{
    public class Pellet : Item
    {

        public LayerMask playerLayer;

        protected virtual void Eat()
        {
            GameManager.Instance.PelletEaten(this);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == playerLayer.value) {
                Eat();
            }
        }

    }

}
