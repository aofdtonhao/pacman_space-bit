using UnityEngine;
namespace Tonhex
{

    public class PowerPellet : Pellet
    {

        [SerializeField]
        private float duration = 8f;
        public float Duration => duration;

        public override void Scored()
        {
            GameManager.Instance.PowerPelletEaten(this);
        }

    }

}
