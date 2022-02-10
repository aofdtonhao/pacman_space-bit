using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Collider2D))]
    public abstract class Item : MonoBehaviour, IScorable
    {

        public int ScorePoints { get; set; }

        public abstract void Scored();

    }

}
