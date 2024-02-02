using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Generic;

namespace Utility.Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSource backgroundMusicSource;
        public AudioClip[] soundEffectClips;
        public AudioClip[] backgroundMusicClips;
        private GameObjectPool<AudioSource> _audioSourcePool;
        
        [SerializeField] private float soundEffectVolume = 1f;

        protected override void Awake()
        {
            base.Awake();
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        }

        private void Start()
        {
            
            GameObject audioSourcePrefab = new GameObject("PooledAudioSource");
            audioSourcePrefab.AddComponent<AudioSource>();
            audioSourcePrefab.SetActive(false);

            _audioSourcePool = new GameObjectPool<AudioSource>(audioSourcePrefab, 10);
        }

        public void PlaySFX(string name)
        {
            foreach (var clip in soundEffectClips)
            {
                if (clip.name == name)
                {
                    AudioSource audioSource = _audioSourcePool.Get();
                    audioSource.clip = clip;
                    audioSource.volume = soundEffectVolume;
                    audioSource.Play();
                    StartCoroutine(ReturnAudioSourceToPool(audioSource, clip.length));
                    return;
                }
            }

            Debug.Log("Sound effect not found: " + name);
        }

        private IEnumerator ReturnAudioSourceToPool(AudioSource audioSource, float delay)
        {
            yield return new WaitForSeconds(delay);
            _audioSourcePool.Release(audioSource);
        }
        
        public void PlayBGM(string name)
        {
            foreach (var clip in backgroundMusicClips)
            {
                if (clip.name == name)
                {
                    backgroundMusicSource.clip = clip;
                    backgroundMusicSource.Play();
                    return;
                }
            }
            Debug.Log("Background music not found: " + name);
        }
        
        public void SetSFXVolume(float volume)
        {
            soundEffectVolume  = volume;
        }

        public void SetBGMVolume(float volume)
        {
            backgroundMusicSource.volume = volume;
        }
        
        public float SfxVolume => soundEffectVolume;
        
        public float BgmVolume => backgroundMusicSource.volume;
    }
}
