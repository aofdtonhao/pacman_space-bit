using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public enum Teste : int
    {
        OPA,
        SEI,
        TODO
    }

    public static AudioManager instance { get; private set; } = null;

    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource effectAudioSource;

    [SerializeField]
    private float lowPitchRange = 0.95f;
    [SerializeField]
    private float highPitchRange = 1.05f;

    [SerializeField]
    private AudioClip[] audioClips = new AudioClip[System.Enum.GetValues(typeof(Teste)).Length];

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayEffect(Teste clip, float pitch = 1f)
    {
        effectAudioSource.pitch = pitch;
        effectAudioSource.clip = audioClips[(int)clip];
        effectAudioSource.Play();
    }


    public void PlayEffectRandomPitch(Teste clip)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        PlayEffect(clip, randomPitch);
    }

}
