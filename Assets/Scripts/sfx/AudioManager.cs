using System;
using sfx;
using UnityEngine;

namespace SFX
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private Sound[] weaponSounds;
        public static AudioManager instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            foreach (Sound audioSource in weaponSounds)
            {
                audioSource.source = gameObject.AddComponent<AudioSource>();
                audioSource.source.clip = audioSource.soundClip;
                audioSource.source.loop = audioSource.loop;
            }
        }

        public void Play(string audioName)
        {
            Sound audioSource = Array.Find(weaponSounds, sound => sound.soundName == audioName);
            audioSource.source.Play();
        }
        
        public void PlayOneShot(string audioName)
        {
            Sound audioSource = Array.Find(weaponSounds, sound => sound.soundName == audioName);
            audioSource.source.PlayOneShot(audioSource.soundClip);
        }

        public void Stop(string audioName)
        {
            Sound audioSource = Array.Find(weaponSounds, sound => sound.soundName == audioName);
            audioSource.source.Stop();
        }

        public void ChangeMasterVolume(float value)
        {
            AudioListener.volume = value;
        }

    }
}
