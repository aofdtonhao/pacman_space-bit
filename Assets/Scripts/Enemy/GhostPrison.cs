using System.Collections;
using UnityEngine;

namespace Tonhex
{

    public class GhostPrison : GhostBehaviour
    {

        public Transform inside;
        public Transform outside;

        void OnEnable()
        {
            StopAllCoroutines();
        }

        void OnDisable()
        {
            // Check for active self to prevent error when object is destroyed
            if (gameObject.activeSelf) {
                StartCoroutine(ExitTransition());
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // Reverse direction everytime the ghost hits a wall to create the
            // effect of the ghost bouncing around the home
            if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
                GhostEnemy.CharacterMovement.SetDirection(-GhostEnemy.CharacterMovement.direction);
            }
        }

        private IEnumerator ExitTransition()
        {
            // Turn off movement while we manually animate the position
            GhostEnemy.CharacterMovement.SetDirection(Vector2.up, true);
            GhostEnemy.CharacterMovement.rigidbody.isKinematic = true;
            GhostEnemy.CharacterMovement.enabled = false;

            Vector3 position = transform.position;

            float duration = 0.5f;
            float elapsed = 0f;

            // Animate to the starting point
            while (elapsed < duration) {
                GhostEnemy.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0f;

            // Animate exiting the ghost home
            while (elapsed < duration) {
                GhostEnemy.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Pick a random direction left or right and re-enable movement
            GhostEnemy.CharacterMovement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
            GhostEnemy.CharacterMovement.rigidbody.isKinematic = false;
            GhostEnemy.CharacterMovement.enabled = true;
        }

    }

}