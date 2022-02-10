using UnityEngine;

namespace Tonhex
{

    public abstract class Enemy : Character, IScorable
    {

        [SerializeField]
        private Transform target;
        public Transform Target => target;

        [SerializeField]
        private int scorePoints;
        public int ScorePoints => scorePoints;

        public abstract void Scored();

    }

}
