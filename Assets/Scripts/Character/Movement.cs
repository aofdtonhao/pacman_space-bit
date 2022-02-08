using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {

        public float speed = 8f;
        public float speedMultiplier = 1f;
        public Vector2 initialDirection;

        public new Rigidbody2D rigidbody { get; private set; }
        public Vector2 direction { get; private set; }
        public Vector2 nextDirection { get; private set; }
        public Vector3 startingPosition { get; private set; }

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            startingPosition = transform.position;
        }

        void Start()
        {
            ResetState();
        }

        void FixedUpdate()
        {
            Vector2 position = rigidbody.position;
            Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

            rigidbody.MovePosition(position + translation);
        }

        void Update()
        {
            if (nextDirection != Vector2.zero) {
                SetDirection(nextDirection);
            }
        }

        public void ResetState()
        {
            speedMultiplier = 1f;
            direction = initialDirection;
            nextDirection = Vector2.zero;
            transform.position = startingPosition;
            rigidbody.isKinematic = false;
            enabled = true;
        }

        public void SetDirection(Vector2 direction, bool forced = false)
        {
            if (forced || !Occupied(direction)) {
                this.direction = direction;
                nextDirection = Vector2.zero;
            } else {
                nextDirection = direction;
            }
        }

        public bool Occupied(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, GameManager.Instance.mazeLayer);
            return (hit.collider != null);
        }

    }

}
