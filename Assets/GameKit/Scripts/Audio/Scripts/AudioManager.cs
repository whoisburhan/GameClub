using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.GameKit
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        public AudioSource backgroundAudio;
        public AudioSource[] audioSource;

        [Header("Audio Clip")]
        public AudioClip[] backgroundClip;
        public AudioClip[] sounds;

        private void Awake()
        {
            if (Instance != null)
                Destroy(this.gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_audio"></param>
        public void Play(SoundName _audio)
        {
            audioSource[0].PlayOneShot(sounds[((int)_audio)]);
        }

        /// <summary>
        /// Set and Play Background Music
        /// </summary>
        /// <param name="_audioClipIndex"></param>
        public void BackgroundAudioFunc(MusicName _music, bool _isLoop = true /*, float _pitch = 1f*/)
        {
            if (backgroundAudio.isPlaying)
            {
                backgroundAudio.Stop();
            }

            //backgroundAudio.pitch = _pitch;
            // backgroundAudio.volume = _volume;
            backgroundAudio.loop = _isLoop;
            backgroundAudio.clip = backgroundClip[((int)_music)];
            backgroundAudio.Play();
        }

        /// <summary>
        /// Set and play Sounds
        /// </summary>
        /// <param name="_audioSource"></param>
        /// <param name="_audioClipIndex"></param>
        public void AudioChangeFunc(SoundName _audio, int _audioSource = 0, bool _isLoop = false/*, float _pitch = 1f, float _volume = 1f*/)
        {
            if (audioSource[_audioSource].isPlaying)
            {
                audioSource[_audioSource].Stop();
            }

            //  audioSource[_audioSource].pitch = _pitch;
            // audioSource[_audioSource].volume = _volume;
            audioSource[_audioSource].loop = _isLoop;
            audioSource[_audioSource].clip = sounds[((int)_audio)];
            audioSource[_audioSource].Play();
        }

        /// <summary>
        /// Stop BackGround Music
        /// </summary>
        public void StopBackgroundMusic()
        {
            if (backgroundAudio.isPlaying)
            {
                backgroundAudio.Stop();
            }
        }

        public bool IsSoundMute()
        {
            return audioSource[0].mute;
        }

        public bool IsMusicMute()
        {
            return backgroundAudio.mute;
        }
    }

    public enum SoundName
    {
        CLICK = 0, GAME_OVER, LEVEL_COMPLETE, GAME_OVER_2, CLICK_2, CLICK_3, CLICK_4, CLICK_5, GAME_OVER_3, SUCCESS, GAME_OVER_4, COIN,SHAKE
    }

    public enum MusicName
    {
        BEN_SOUND_BUDDY = 0
    }
}