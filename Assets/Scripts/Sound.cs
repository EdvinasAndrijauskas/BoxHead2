using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class Sound {
        public string soundName;
        public AudioClip soundClip;
        [HideInInspector]
        public AudioSource source;
    }
}