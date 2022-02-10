using System.Collections;
using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Ghost))]
    public abstract class GhostBehaviour : MonoBehaviour
    {

        public const string ANIMATION_BOOL_FLASH = "FlashBool";

        [SerializeField]
        private float duration = 0f;
        public float Duration => duration;

        public Ghost GhostEnemy;

        public Animator GhostAnimator;

        public void Enable()
        {
            Enable(duration);
        }

        public virtual void Enable(float duration)
        {
            enabled = true;

            StopAllCoroutines();
            if (duration > 0f)
            {
                StartCoroutine(WaitAndDisable(duration));
            }
            else
            {
                Disable();
            }
        }

        public virtual void Disable()
        {
            // TODO: StopAllCoroutines();

            enabled = false;
        }

        IEnumerator WaitAndDisable(float duration)
        {
            yield return new WaitForSeconds(duration);

            Disable();
        }

    }

}
