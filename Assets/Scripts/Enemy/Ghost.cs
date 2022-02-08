using UnityEngine;

namespace Tonhex
{

    public class Ghost : Enemy, IScorable
    {

        public const string WHITE_ANIMATION = "White";
        public const string BLUE_ANIMATION = "Blue";

        public GhostFrightened frightened { get; private set; }
        // TODO: public GhostPrison prison { get; private set; }

        public int scorePoints { get; set; }

        void Awake()
        {
            Debug.Log("Ghost::Awake() - TODO");
        }

        void Start()
        {
            ResetState();
        }

        public void ResetState()
        {
            gameObject.SetActive(true);
            movement.ResetState();
        }

        public void SetPosition(Vector3 position)
        {
            position.z = transform.position.z;
            transform.position = position;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == GameManager.Instance.playerLayer.value) {
                if (frightened.enabled) {
                    GameManager.Instance.GhostEaten(this);
                } else {
                    GameManager.Instance.PacmanDeath();
                }
            }
        }

        public void Scored()
        {
            // ghost.SetPosition(ghost.home.inside.position);
            // ghost.home.Enable(duration);
        }

    }

}
