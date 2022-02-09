using UnityEngine;

namespace Tonhex
{

    public class Pacman : Player
    {

        public SpriteRenderer spriteRenderer { get; private set; }
        public new Collider2D collider { get; private set; }

        private Animator animator;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<Collider2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            int inputX = (int)Input.GetAxisRaw("Horizontal");
            int inputY = 0;

            if (inputX != 0) {
                inputY = (int)Input.GetAxisRaw("Vertical");
            }

            if (inputX != 0 || inputY != 0) {
                movement.SetDirection(new Vector3(inputX, inputY));
                transform.rotation = Quaternion.FromToRotation(movement.direction, transform.forward);
            }
        }

        public void ResetState()
        {
            enabled = true;
            spriteRenderer.enabled = true;
            collider.enabled = true;
            animator.SetBool(ANIMATION_BOOL_DEATH, false);
            movement.ResetState();
            gameObject.SetActive(true);
        }

        public void DeathSequence()
        {
            enabled = false;
            spriteRenderer.enabled = false;
            collider.enabled = false;
            movement.enabled = false;
            animator.SetBool(ANIMATION_BOOL_DEATH, true);
        }

    }

}
