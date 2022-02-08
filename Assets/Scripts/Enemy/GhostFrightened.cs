using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Ghost))]
    public class GhostFrightened : EnemyBehavior
    {

        public SpriteRenderer body;
        public SpriteRenderer eyes;
        public SpriteRenderer blue;
        public SpriteRenderer white;

        public Ghost ghost { get; private set; }

        public bool eaten { get; private set; }

        public override void Enable(float duration)
        {
            base.Enable(duration);

            body.enabled = false;
            eyes.enabled = false;
            blue.enabled = true;
            white.enabled = false;

            Invoke(nameof(Flash), duration / 2f);
        }

        public override void Disable()
        {
            base.Disable();

            body.enabled = true;
            eyes.enabled = true;
            blue.enabled = false;
            white.enabled = false;
        }

        private void Start()
        {
            ghost = (Ghost)enemy;
        }

        private void Eaten()
        {
            eaten = true;
            ghost.Scored();

            body.enabled = false;
            eyes.enabled = true;
            blue.enabled = false;
            white.enabled = false;
        }

        private void Flash()
        {
            if (!eaten) {
                blue.enabled = false;
                white.enabled = true;
                white.GetComponent<Animator>().SetBool(Ghost.WHITE_ANIMATION, true);
            }
        }

        void OnEnable()
        {
            blue.GetComponent<Animator>().SetBool(Ghost.BLUE_ANIMATION, true);
            enemy.movement.speedMultiplier = 0.5f;
            eaten = false;
        }

        void OnDisable()
        {
            enemy.movement.speedMultiplier = 1f;
            eaten = false;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Node node = other.GetComponent<Node>();

            if (node != null && enabled) {
                Vector2 direction = Vector2.zero;
                float maxDistance = float.MinValue;

                foreach (Vector2 availableDirection in node.availableDirections) {
                    Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                    float distance = (ghost.target.position - newPosition).sqrMagnitude;

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
