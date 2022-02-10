using UnityEngine;

namespace Tonhex
{


    public class GhostFrightened : GhostBehaviour
    {

        public const string ANIMATION_BOOL_FLASH = "Flash";

        public const float SPEED_MULTIPLIER_DEFAULT = 1f;
        public const float SPEED_MULTIPLIER_FLASH = SPEED_MULTIPLIER_DEFAULT / 2f;

        public bool IsEaten { get; private set; }

        public override void Enable(float duration)
        {
            base.Enable(duration);

            GhostAnimator.SetBool(ANIMATION_BOOL_FLASH, true);
        }

        public override void Disable()
        {
            base.Disable();

            GhostAnimator.SetBool(ANIMATION_BOOL_FLASH, false);
        }

        private void Eaten()
        {
            IsEaten = true;

            GhostEnemy.Scored();

            GhostAnimator.SetBool(Character.ANIMATION_BOOL_DEATH, true);
        }

        void OnEnable()
        {
            GhostEnemy.CharacterMovement.speedMultiplier = SPEED_MULTIPLIER_FLASH;

            IsEaten = false;
        }

        void OnDisable()
        {
            GhostEnemy.CharacterMovement.speedMultiplier = SPEED_MULTIPLIER_DEFAULT;

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
                    float distance = (GhostEnemy.target.position - newPosition).sqrMagnitude;

                    if (distance > maxDistance) {
                        direction = availableDirection;
                        maxDistance = distance;
                    }
                }

                GhostEnemy.CharacterMovement.SetDirection(direction);
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
