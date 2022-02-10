using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {

        public const float SPEED_MULTIPLIER_DEFAULT = 1f;

        public float speed = 8f;

        [HideInInspector]
        public float speedMultiplier;

        public Vector2 initialDirection;

        public Rigidbody2D chupeta;

        public Vector2 direction { get; /*private*/ set; }
        public Vector2 nextDirection { get; private set; }
        public Vector3 startingPosition { get; private set; }

        void Awake()
        {
            chupeta = GetComponent<Rigidbody2D>();
            startingPosition = transform.position;
        }

        void Start()
        {
            ResetState();
        }

        void FixedUpdate()
        {
            Vector2 position = chupeta.position;
            Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

            Debug.Log("speed * speedMultiplier: " + speed * speedMultiplier);
            Debug.Log("position: " + position);
            Debug.Log("translation: " + translation);
            Debug.Log("direction: " + direction);
            Debug.Log("rigidbody: " + chupeta.name);

            chupeta.MovePosition(position + translation);
        }

        void Update()
        {
            if (nextDirection != Vector2.zero)
            {
                SetDirection(nextDirection);
            }
        }

        public void ResetState()
        {
            Debug.Log("SPECIAL PLACE: " + initialDirection);

            speedMultiplier = 1f;
            direction = initialDirection;
            nextDirection = Vector2.zero;
            transform.position = startingPosition;
            chupeta.isKinematic = false;
            enabled = true;
        }

        public void SetDirection(Vector2 direction, bool forced = false)
        {
            if (forced || !Occupied(direction))
            {
                this.direction = direction;
                nextDirection = Vector2.zero;

                Debug.Log("SetDirection:: forced || !Occupied(direction)");
            }
            else
            {
                Debug.Log("SetDirection:: else");

                nextDirection = direction;
            }
        }

        public bool Occupied(Vector2 direction)
        {
            // If no collider is hit then there is no obstacle in that direction
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, GameManager.Instance.MazeLayer);
            return hit.collider != null;
        }

    }

}
