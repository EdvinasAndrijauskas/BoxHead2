using UnityEngine;

namespace model.ammo
{
    public class Ammo : MonoBehaviour
    {
        private string ammoName;
        public float ammoDamage;
        public AudioSource sound { get; set; }

        protected Ammo(string ammoName, float ammoDamage, AudioSource sound)
        {
            this.ammoName = ammoName;
            this.ammoDamage = ammoDamage;
            this.sound = sound;
        }
            
    }
}