using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Ghost))]
    public class GhostBehaviour : EnemyBehaviour
    {

        public Ghost ghost { get; private set; }

        void Start()
        {
            ghost = (Ghost)enemy;
        }

    }

}
