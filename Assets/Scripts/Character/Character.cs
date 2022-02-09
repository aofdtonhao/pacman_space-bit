using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Movement))]
    public class Character : MonoBehaviour
    {

        public const string ANIMATION_BOOL_DEATH = "Death";

        public Movement movement { get; private set; }

        void Awake()
        {
            movement = GetComponent<Movement>();
        }

    }

}
