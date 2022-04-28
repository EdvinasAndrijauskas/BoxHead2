using System;
using UnityEngine;

namespace model
{
    public class Laser : MonoBehaviour
    {
        private Vector3 _startPosition;
        private Vector2 _endPosition;
        private float _progress;

        [SerializeField] private float _speed = 40f;
        void Start()
        {
            _startPosition = transform.position;
        }
        void Update()
        {
            _progress += Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(_startPosition, _endPosition, _progress);
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            _endPosition = targetPosition;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.tag.Equals("Enemy"))
            {
                col.gameObject.GetComponent<ZombieHealth>().Damage(100);
                if (col.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
                {
                    Destroy(col.gameObject);
                }
            }
        }
    }
}