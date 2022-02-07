using UnityEngine;

namespace Tonhex
{
    public class Pellet : Item
    {

        public int points = 10;

        protected virtual void Eat()
        {
            GameManager.instance.PelletEaten(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
                Eat();
            }
        }

    }

}
