using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Animator))]
    public abstract class Player : Character
    {

        public Animator PlayerAnimator { get; protected set; }

    }

}
