using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Movement))]
    public abstract class Character : MonoBehaviour
    {

        public const string ANIMATION_BOOL_DEATH = "Death";

        public Movement CharacterMovement { get; protected set; }

        protected virtual void Awake()
        {
            CharacterMovement = GetComponent<Movement>();
        }

    }

}
