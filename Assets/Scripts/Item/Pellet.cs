using UnityEngine;

namespace Tonhex
{
    public class Pellet : Item
    {

        public override void Scored()
        {
            GameManager.Instance.PelletEaten(this);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == GameManager.Instance.playerLayer.value) {
                Scored();
            }
        }

    }

}
