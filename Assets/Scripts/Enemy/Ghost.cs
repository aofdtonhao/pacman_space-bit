using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonhex
{

    public class Ghost : Enemy
    {

        public Animator GhostAnimator { get; protected set; }

        public GhostFrightened Frightened { get; private set; }
        public GhostPrison Prison { get; private set; }

        [SerializeField]
        private GhostBehaviour defaultBehaviour = null;

        protected override void Awake()
        {
            base.Awake();

            GhostAnimator = GetComponent<Animator>();

            Frightened = GetComponent<GhostFrightened>();
            Prison = GetComponent<GhostPrison>();
        }

        void Start()
        {
            ResetState();
        }

        public void ResetState()
        {
            gameObject.SetActive(true);
            CharacterMovement.ResetState();

            foreach (AnimatorControllerParameter parameter in GhostAnimator.parameters) {
                if (parameter.type == AnimatorControllerParameterType.Bool) {
                    GhostAnimator.SetBool(parameter.name, false);
                }
            }
            // TODO: GhostAnimator.SetBool(ANIMATION_BOOL_DEATH, false);
            // TODO: GhostAnimator.SetBool(GhostBehaviour.ANIMATION_BOOL_FLASH, false);

            foreach (GhostBehaviour ghostBehaviour in GetComponents<GhostBehaviour>()) {
                if (ghostBehaviour != defaultBehaviour) {
                    defaultBehaviour.Disable();
                }
            }

            if (defaultBehaviour != null) {
                defaultBehaviour.Enable();
            }
        }

        public void SetPosition(Vector3 position)
        {
            position.z = transform.position.z;
            transform.position = position;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == GameManager.Instance.PlayerLayer.value) {
                if (Frightened.enabled) {
                    GameManager.Instance.GhostEaten(this);
                } else {
                    GameManager.Instance.PacmanDeath();
                }
            }
        }

        public override void Scored()
        {
            SetPosition(Prison.inside.position);
            Prison.Enable(Prison.Duration);
        }

    }

}
