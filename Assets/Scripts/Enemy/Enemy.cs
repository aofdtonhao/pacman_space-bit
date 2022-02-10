using UnityEngine;

namespace Tonhex
{

    public class Enemy : Character
    {

        [SerializeField]
        private Transform target;
        public Transform Target => target;

        [SerializeField]
        private int scorePoints;
        public int ScorePoints => scorePoints;

    }

}
