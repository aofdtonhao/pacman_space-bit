using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Animator))]
    public abstract class Player : Character
    {

        public Animator PlayerAnimator { get; protected set; }

        protected override void Awake()
        {
            base.Awake();

            PlayerAnimator = GetComponent<Animator>();
        }

    }

}
