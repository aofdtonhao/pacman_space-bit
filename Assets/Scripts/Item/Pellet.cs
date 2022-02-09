using UnityEngine;

namespace Tonhex
{
    public class Pellet : Item
    {

        protected virtual void Eat()
        {
            GameManager.Instance.PelletEaten(this);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == GameManager.Instance.playerLayer.value) {
                Eat();
            }
        }

    }

}
