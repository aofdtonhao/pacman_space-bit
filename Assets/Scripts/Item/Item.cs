using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Collider2D))]
    public abstract class Item : MonoBehaviour, IScorable
    {

        [SerializeField]
        private int scorePoints;
        public int ScorePoints => scorePoints;

        public abstract void Scored();

    }

}
