using UnityEngine;

namespace Tonhex
{

    public class Ghost : Enemy, IScorable
    {

        public int ScorePoints { get; set; }

        public GhostFrightened Frightened { get; private set; }
        public GhostPrison Prison { get; private set; }

        void Awake()
        {
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
                if (Frightened.enabled) {
                    GameManager.Instance.GhostEaten(this);
                } else {
                    GameManager.Instance.PacmanDeath();
                }
            }
        }

        public void Scored()
        {
            SetPosition(Prison.inside.position);
            Prison.Enable();
        }

    }

}
