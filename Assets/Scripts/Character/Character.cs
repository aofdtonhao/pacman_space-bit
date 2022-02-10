using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Movement))]
    public class Character : MonoBehaviour
    {

        public const string ANIMATION_BOOL_DEATH = "DeathBool";

        public Movement CharacterMovement;

    }

}
