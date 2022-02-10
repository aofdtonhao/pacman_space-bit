using System.Collections;
using UnityEngine;

namespace Tonhex
{

    [RequireComponent(typeof(Ghost))]
    public abstract class GhostBehaviour : MonoBehaviour
    {

        public const string ANIMATION_BOOL_FLASH = "Flash";

        [SerializeField]
        private float duration = 0f;
        public float Duration => duration;

        public Ghost GhostEnemy { get; protected set; }

        public Animator GhostAnimator { get; protected set; }


        protected virtual void Awake()
        {
            GhostEnemy = GetComponent<Ghost>();
            GhostAnimator = GetComponent<Animator>();
        }

        public void Enable()
        {
            Enable(duration);
        }

        public virtual void Enable(float duration)
        {
            enabled = true;

            StopAllCoroutines();
            StartCoroutine(WaitAndDisable(duration));
        }

        public virtual void Disable()
        {
            enabled = false;

            StopAllCoroutines();
        }

        IEnumerator WaitAndDisable(float duration)
        {
            yield return new WaitForSeconds(duration);

            Disable();
        }

    }

}
