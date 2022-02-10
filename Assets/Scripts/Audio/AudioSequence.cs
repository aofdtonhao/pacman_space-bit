
using UnityEngine;

namespace Tonhex
{

    public class AudioSequence
    {

        private readonly AudioClip[] audioClips;

        private int currentAudioClipIndex = 0;

        public AudioSequence(AudioClip[] audioClips)
        {
            currentAudioClipIndex = 0;

            this.audioClips = audioClips;
        }

        public AudioClip GetCurrentAudioClip()
        {
            AudioClip currentAudioClip = audioClips[currentAudioClipIndex];
            currentAudioClipIndex = (currentAudioClipIndex + 1) % audioClips.Length;
            return currentAudioClip;
        }

    }

}
