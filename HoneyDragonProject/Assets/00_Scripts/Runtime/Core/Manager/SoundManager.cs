using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum SoundType
{
    BGM,
    Effect,
    MaxCount
}
namespace RPG.Core.Manager
{
    public class SoundManager : IManager
    {
        private AudioSource[] audioSources;
        private Dictionary<string, AudioClip> audioClips;
        private GameObject SoundManagerObject;

        public float MasterVolume { get; set; }
        public float BgmVolume { get; set; }
        public float EffectVolume { get; set; }

        public void Init()
        {
            audioSources = new AudioSource[(int)SoundType.MaxCount];
            MasterVolume = 1f;
            audioClips = new Dictionary<string, AudioClip>();
            SoundManagerObject = new GameObject();
            SoundManagerObject.name = "@SoundManager";
            MonoBehaviour.DontDestroyOnLoad(SoundManagerObject);

            for (int i = 0; i < (int)SoundType.MaxCount; ++i)
            {
                audioSources[i] = SoundManagerObject.AddComponent<AudioSource>();
            }

            audioSources[(int)SoundType.BGM].loop = true;
        }

        public void StopBGM()
        {
            audioSources[(int)SoundType.BGM].Stop();
        }

        public void PauseBGM()
        {
            audioSources[(int)SoundType.BGM].Pause();
        }

        public void ResumeBGM()
        {
            audioSources[(int)SoundType.BGM].Play();
        }

        public void PlaySound(SoundType type, string path)
        {
            switch (type)
            {
                case SoundType.BGM:
                    audioSources[(int)SoundType.BGM].Stop();
                    audioSources[(int)SoundType.BGM].clip = GetClip(path);
                    audioSources[(int)SoundType.BGM].Play();
                    break;
                case SoundType.Effect:
                    audioSources[(int)SoundType.Effect].PlayOneShot(GetClip(path), 1f);
                    break;
                case SoundType.MaxCount:
                    break;
            }
        }

        public void MasterVolumeChange(float volume)
        {
            volume = Mathf.Clamp01(volume);
            MasterVolume = volume;
        }
        public void VolumeChange(SoundType type, float volume)
        {
            volume = Mathf.Clamp01(volume);
            switch (type)
            {
                case SoundType.BGM:
                    // audioSources
                    break;
                case SoundType.Effect:
                    break;
            }
        }

        public void PlaySoundAtPosition(string path, Vector3 position)
        {
            AudioSource.PlayClipAtPoint(GetClip(path),position, 1f);
        }

        private AudioClip GetClip(string path)
        {
            if(audioClips.TryGetValue(path, out var clip) == false)
            {
                clip = Util.ResourceCache.Load<AudioClip>(path);
                audioClips.Add(path, clip);
                return clip;
            }
            else
            {
                return clip;
            }
        }

        public void Clear()
        {
            foreach(var audioSource in audioSources)
            {
                audioSource.clip = null;
                audioSource.Stop();
            }

            audioClips.Clear();
            MonoBehaviour.Destroy(SoundManagerObject.gameObject);
        }

    }
}
