using UnityEngine;

namespace Tonhex
{

    public class AudioManager : MonoBehaviour
    {



        [SerializeField]
        private AudioSource TODOAudioSource;
        [SerializeField]
        private AudioSource effectAudioSource;

        void Awake()
        {
            Debug.Log("AudioManager::Awake()");
        }

        void Start()
        {
            Debug.Log("AudioManager::Start()");
        }

    }

}
