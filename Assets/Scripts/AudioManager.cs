using UnityEngine;

namespace Tonhex
{

    public class AudioManager : MonoBehaviour
    {

        public const float EFFECT_PITCH_AMOUNT = 0.05f;
        public const float EFFECT_PITCH_MULTIPLIER = 2f;
        public const float EFFECT_PITCH_DEFAULT = 1f;
        public const float EFFECT_PITCH_DEFAULT_LOW = EFFECT_PITCH_DEFAULT - EFFECT_PITCH_AMOUNT;
        public const float EFFECT_PITCH_DEFAULT_HIGH = EFFECT_PITCH_DEFAULT + EFFECT_PITCH_AMOUNT;
        public const float EFFECT_PITCH_MIN = EFFECT_PITCH_DEFAULT - (EFFECT_PITCH_AMOUNT * EFFECT_PITCH_MULTIPLIER);
        public const float EFFECT_PITCH_MAX = EFFECT_PITCH_DEFAULT + (EFFECT_PITCH_AMOUNT * EFFECT_PITCH_MULTIPLIER);

        public static AudioManager Instance { get; private set; } = null;

        [SerializeField]
        private AudioSource loopAudioSource;
        [SerializeField]
        private AudioSource effectAudioSource;

        [SerializeField]
        [Range(EFFECT_PITCH_MIN, EFFECT_PITCH_DEFAULT)]
        private float pitchLowRange = EFFECT_PITCH_DEFAULT_LOW;
        [SerializeField]
        [Range(EFFECT_PITCH_DEFAULT, EFFECT_PITCH_MAX)]
        private float pitchHighRange = EFFECT_PITCH_DEFAULT_HIGH;

        public AudioClip[] PalletEffectAudioClips { get; private set; }
        public AudioClip PowerPalletEffectAudioClip { get; private set; }
        public AudioClip EatGhostEffectAudioClip { get; private set; }
        public AudioClip EatFruitEffectAudioClip { get; private set; }
        public AudioClip PlayerDeathEffectAudioClip { get; private set; }

        private int currentAudioClipIndex = 0;

        void Awake()
        {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            currentAudioClipIndex = 0;

            effectAudioSource.pitch = EFFECT_PITCH_DEFAULT;
        }

        public void PlayLoop(AudioClip audioClip)
        {
            loopAudioSource.loop = true;
            loopAudioSource.clip = audioClip;
            loopAudioSource.Play();
        }

        public void StopLoop()
        {
            loopAudioSource.Stop();
        }

        public void StopLoopAndPlay(AudioClip audioClip)
        {
            StopLoop();
            PlayLoop(audioClip);
        }

        public void PlayEffect(AudioClip audioClip, float pitch = 1f)
        {
            effectAudioSource.pitch = pitch;
            effectAudioSource.clip = audioClip;
            effectAudioSource.Play();
        }

        public void PlayEffect(params AudioClip[] audioClips)
        {
            int randomIndex = Random.Range(0, audioClips.Length);

            PlayEffect(audioClips[randomIndex]);
        }

        public void PlayEffectRandomPitch(AudioClip audioClip)
        {
            float randomPitch = Random.Range(pitchLowRange, pitchHighRange);

            PlayEffect(audioClip, randomPitch);
        }

        public void PlayEffectRandomPitch(params AudioClip[] audioClips)
        {
            int randomIndex = Random.Range(0, audioClips.Length);

            PlayEffectRandomPitch(audioClips[randomIndex]);
        }

        public void PlayEffectSequence(AudioClip[] audioClips)
        {
            PlayEffectRandomPitch(audioClips[currentAudioClipIndex]); // TODO

            currentAudioClipIndex = (currentAudioClipIndex + 1) % audioClips.Length;
        }

    }

}