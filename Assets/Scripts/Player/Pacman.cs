using UnityEngine;

namespace Tonhex
{

    public class Pacman : Player
    {

        public SpriteRenderer spriteRenderer { get; private set; }
        public new Collider2D collider { get; private set; }

        void Awake()
        {
            // CharacterMovement = GetComponent<Movement>();

            PlayerAnimator = GetComponent<Animator>();

            spriteRenderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider2D>();
        }

        void Update()
        {
            int inputX = (int)Input.GetAxisRaw("Horizontal");
            int inputY = 0;

            if (inputX == 0)
            {
                inputY = (int)Input.GetAxisRaw("Vertical");
            }

            if (inputX != 0 || inputY != 0)
            {
                Debug.Log("inputX: " + inputX + "inputY: " + inputY);

                CharacterMovement.SetDirection(new Vector2(inputX, inputY));
            }

            float angle = Mathf.Atan2(CharacterMovement.direction.y, CharacterMovement.direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        }

        public void ResetState()
        {
            enabled = true;
            spriteRenderer.enabled = true;
            collider.enabled = true;
            CharacterMovement.ResetState();
            gameObject.SetActive(true);

            foreach (AnimatorControllerParameter parameter in PlayerAnimator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    PlayerAnimator.SetBool(parameter.name, false);
                }
            }
        }
        public void DeathSequence()
        {
            enabled = false;
            spriteRenderer.enabled = false;
            collider.enabled = false;
            CharacterMovement.enabled = false;
            PlayerAnimator.SetBool(ANIMATION_BOOL_DEATH, true);
        }

    }

}
