using UnityEngine;

namespace Tonhex
{

    public class AudioManager : MonoBehaviour
    {

        public static AudioManager instance { get; private set; } = null;

        [SerializeField]
        private AudioSource musicAudioSource;
        [SerializeField]
        private AudioSource effectAudioSource;

        [SerializeField]
        private float lowPitchRange = 0.95f;
        [SerializeField]
        private float highPitchRange = 1.05f;

        void Awake()
        {
            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void PlayEffect(AudioClip clip)
        {
            effectAudioSource.pitch = 1f;
            effectAudioSource.clip = clip;
            effectAudioSource.Play();
        }


        public void PlayEffectRandomPitch(AudioClip clip)
        {
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            effectAudioSource.pitch = randomPitch;
            effectAudioSource.clip = clip;
            effectAudioSource.Play();
        }

    }

}
