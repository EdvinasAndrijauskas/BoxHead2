﻿using UnityEngine;

namespace model.ammo
{
    public class Laser : MonoBehaviour, IAmmoDamage
    {
        private Vector3 _startPosition;
        private Vector2 _endPosition;
        private float _progress;
        
        [SerializeField] private float ammoDamage;

        void Start()
        {
            _startPosition = transform.position;
        }
        void Update()
        {
            _progress += Time.deltaTime * 40;
            transform.position = Vector3.Lerp(_startPosition, _endPosition, _progress);
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            _endPosition = targetPosition;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
           TakeDamage(col.collider,ammoDamage);
        }

        public void TakeDamage(Collider2D col, float damage)
        {
            if (col.tag.Equals("Enemy"))
            {
                col.gameObject.GetComponent<ZombieHealth>().Damage(damage);
                if (col.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
                {
                    Destroy(col.gameObject);
                }
            }
            
        }
    }
}