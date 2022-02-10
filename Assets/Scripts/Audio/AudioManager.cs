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
        private AudioSource musicAudioSource;
        [SerializeField]
        private AudioSource effectAudioSource;

        [SerializeField]
        [Range(EFFECT_PITCH_MIN, EFFECT_PITCH_DEFAULT)]
        private float pitchLowRange = EFFECT_PITCH_DEFAULT_LOW;
        [SerializeField]
        [Range(EFFECT_PITCH_DEFAULT, EFFECT_PITCH_MAX)]
        private float pitchHighRange = EFFECT_PITCH_DEFAULT_HIGH;

        [SerializeField]
        private AudioClip startEffectAudioClip;
        public AudioClip StartEffectAudioClip => StartEffectAudioClip;
        [SerializeField]
        private AudioClip inGameMusicAudioClip;
        public AudioClip InGameMusicAudioClip => inGameMusicAudioClip;

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


        [SerializeField]
        private AudioClip[] palletEffectAudioAudioClips;

        private AudioSequence palletEffectAudioAudioSequence;
        public AudioSequence PalletEffectAudioSequence => palletEffectAudioAudioSequence;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                 Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            effectAudioSource.pitch = EFFECT_PITCH_DEFAULT;

            palletEffectAudioAudioSequence = new AudioSequence(palletEffectAudioAudioClips);

            PlayEffect(startEffectAudioClip);
            PlayMusic(inGameMusicAudioClip, startEffectAudioClip.length);
        }

        public void PlayMusic(AudioClip audioClip, float delay = 0f)
        {
            musicAudioSource.loop = true;
            musicAudioSource.clip = audioClip;

            if (delay > 0f)
            {
                musicAudioSource.PlayDelayed(delay);
            }
            else
            {
                musicAudioSource.Play();
            }
        }

        public void StopMusic()
        {
            musicAudioSource.Stop();
        }

        public void ChangeMusic(AudioClip audioClip)
        {
            StopMusic();
            PlayMusic(audioClip);
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

        public void PlayEffectSequence(AudioSequence audioSequence)
        {
            PlayEffectRandomPitch(audioSequence.GetCurrentAudioClip());
        }

    }

}
