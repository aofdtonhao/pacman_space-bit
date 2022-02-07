// using UnityEngine;

namespace Tonhex
{

    public class PowerPellet : Pellet
    {

        public float duration = 8f;

        protected override void Eat()
        {
            GameManager.instance.PowerPelletEaten(this);
        }

    }

}
