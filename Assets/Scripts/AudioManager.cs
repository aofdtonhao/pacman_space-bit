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

        private AudioClip startLoolAudioClip;
        public AudioClip StartLoolAudioClip => startLoolAudioClip;

        [SerializeField]
        private AudioClip[] palletEffectAudioClips;
        public AudioClip[] PalletEffectAudioClips => palletEffectAudioClips;
        [SerializeField]
        private AudioClip powerPalletEffectAudioClip;
        public AudioClip PowerPalletEffectAudioClip => powerPalletEffectAudioClip;
        [SerializeField]
        private AudioClip eatGhostEffectAudioClip;
        public AudioClip EatGhostEffectAudioClip => eatGhostEffectAudioClip;
        [SerializeField]
        private AudioClip eatFruitEffectAudioClip;
        public AudioClip EatFruitEffectAudioClip => eatFruitEffectAudioClip;
        [SerializeField]
        private AudioClip playerDeathEffectAudioClip;
        public AudioClip PlayerDeathEffectAudioClip => playerDeathEffectAudioClip;

        private int currentAudioClipIndex = 0;

        void Awake()
        {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            Init();
        }

        public void Init()
        {
            currentAudioClipIndex = 0;

            startLoolAudioClip = loopAudioSource.clip;
        }

        void Start()
        {
            effectAudioSource.pitch = EFFECT_PITCH_DEFAULT;

            PlayLoop(startLoolAudioClip);
        }

        public void PlayLoop(AudioClip audioClip)
        {
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

        public void PlayEffect(AudioClip audioClip, float pitch = EFFECT_PITCH_DEFAULT)
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