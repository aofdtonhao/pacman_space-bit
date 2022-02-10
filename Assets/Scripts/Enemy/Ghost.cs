using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonhex
{

    public class Ghost : Enemy, IScorable
    {

        public Animator GhostAnimator { get; protected set; }

        public GhostEyes Eyes { get; private set; }

        public GhostFrightened Frightened { get; private set; }
        public GhostPrison Prison { get; private set; }

        [SerializeField]
        private GhostBehaviour defaultBehaviour = null;

        void Awake()
        {
            CharacterMovement = GetComponent<Movement>();

            GhostAnimator = GetComponent<Animator>();

            Frightened = GetComponent<GhostFrightened>();
            Prison = GetComponent<GhostPrison>();
        }

        void Start()
        {
            Eyes = GetComponentInChildren<GhostEyes>();

            ResetState();
        }

        void Update()
        {
            Eyes.enabled = Frightened.isActiveAndEnabled || Prison.isActiveAndEnabled;
        }

        public void ResetState()
        {
            gameObject.SetActive(true);
            CharacterMovement.ResetState();

            foreach (AnimatorControllerParameter parameter in GhostAnimator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    // GhostAnimator.SetBool(parameter.name, false);
                }
            }

            Frightened.Disable();
            // Prison.Disable();

            if (Prison != defaultBehaviour)
            {
                Debug.Log("Prison == defaultBehaviour");
                // Prison.Disable();
            }

            if (defaultBehaviour != null)
            {
                Debug.Log("defaultBehaviour != null");

                // defaultBehaviour.Enable();
            }
        }

        public void SetPosition(Vector3 position)
        {
            position.z = transform.position.z;
            transform.position = position;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == GameManager.Instance.PlayerLayer.value)
            {
                if (Frightened.enabled)
                {
                    GameManager.Instance.GhostEaten(this);
                }
                else
                {
                    GameManager.Instance.PacmanDeath();
                }
            }
        }

        public void Scored()
        {
            SetPosition(Prison.inside.position);
            Prison.Enable(Prison.Duration);
        }

    }

}
