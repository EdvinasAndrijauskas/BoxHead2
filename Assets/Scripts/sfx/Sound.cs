using UnityEngine;

namespace sfx
{
    [System.Serializable]
    public class Sound {
        public string soundName;
        public AudioClip soundClip;
        [HideInInspector]
        public AudioSource source;
        public bool loop;
    }
}