using System;
using sfx;
using UnityEngine;

namespace SFX
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private Sound[] weaponSounds;
        private static AudioManager _instance;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
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
       
    }
}
