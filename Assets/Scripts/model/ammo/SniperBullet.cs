using System;
using UnityEngine;

namespace model
{
    public class SniperBullet : MonoBehaviour
    {
        private float _progress;
    
        [SerializeField] private float _speed = 30f;
        private void Update()
        {
            _progress += Time.deltaTime * _speed;
            transform.Translate(Vector3.up * _progress);
        }
        

        private void OnTriggerEnter2D(Collider2D col)
        {
            OnTrigger(col);
        }
        

        public void OnTrigger(Collider2D col)
        {
            if (col.tag.Equals("Enemy"))
            {
                col.gameObject.GetComponent<ZombieHealth>().Damage(50);
                Debug.Log(col.gameObject.GetComponent<ZombieHealth>().CurrentHealth + "->>>>>>>>>>>>>>>>>>>>> ZOMBIE DAMAGED TAKEN");

                if (col.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
                {
                    Destroy(col.gameObject);
                }
            }
        }
        void OnBecameInvisible() {
            Destroy(gameObject);
        }
    }
    
    
}