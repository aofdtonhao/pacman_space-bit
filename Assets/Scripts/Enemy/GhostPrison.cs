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
            if (gameObject.activeSelf) {
                StartCoroutine(ExitTransition());
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (enabled && (collision.gameObject.layer == GameManager.Instance.mazeLayer.value)) {
                GhostEnemy.CharacterMovement.SetDirection(-GhostEnemy.CharacterMovement.direction);
            }
        }

        private IEnumerator ExitTransition()
        {
            GhostEnemy.CharacterMovement.SetDirection(Vector2.up, true);
            GhostEnemy.CharacterMovement.rigidbody.isKinematic = true;
            GhostEnemy.CharacterMovement.enabled = false;

            Vector3 position = transform.position;

            float duration = 0.5f;
            float elapsed = 0f;

            while (elapsed < duration) {
                GhostEnemy.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0f;

            while (elapsed < duration) {
                GhostEnemy.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }

            GhostEnemy.CharacterMovement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
            GhostEnemy.CharacterMovement.rigidbody.isKinematic = false;
            GhostEnemy.CharacterMovement.enabled = true;

            GhostAnimator.SetBool(Character.ANIMATION_BOOL_DEATH, false);
            GhostAnimator.SetBool(ANIMATION_BOOL_FLASH, false);
        }

    }

}