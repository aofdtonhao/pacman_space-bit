using UnityEngine;

namespace Tonhex
{


    public class GhostFrightened : GhostBehaviour
    {

        public const string ANIMATION_BOOL_FLASH = "Flash";

        public const float SPEED_MULTIPLIER_DEFAULT = 1f;
        public const float SPEED_MULTIPLIER_FLASH = SPEED_MULTIPLIER_DEFAULT / 2f;

        public bool IsEaten { get; private set; }

        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public override void Enable(float duration)
        {
            base.Enable(duration);

            // TODO: animator.SetBool(ANIMATION_BOOL_FLASH, true);
        }

        public override void Disable()
        {
            base.Disable();

            // TODO: animator.SetBool(ANIMATION_BOOL_FLASH, false);
        }

        private void Eaten()
        {
            IsEaten = true;

            ghost.Scored();

            // TODO: animator.SetBool(Character.ANIMATION_BOOL_DEATH, true);
        }

        void OnEnable()
        {
            enemy.movement.speedMultiplier = SPEED_MULTIPLIER_FLASH;

            IsEaten = false;
        }

        void OnDisable()
        {
            enemy.movement.speedMultiplier = SPEED_MULTIPLIER_DEFAULT;
            IsEaten = false;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Node node = other.GetComponent<Node>();

            if (node != null && enabled) {
                Vector2 direction = Vector2.zero;
                float maxDistance = float.MinValue;

                foreach (Vector2 availableDirection in node.availableDirections) {
                    Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                    float distance = (ghost.Target.position - newPosition).sqrMagnitude;

                    if (distance > maxDistance) {
                        direction = availableDirection;
                        maxDistance = distance;
                    }
                }

                enemy.movement.SetDirection(direction);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == GameManager.Instance.playerLayer.value && enabled) {
                Eaten();
            }
        }

    }

}
