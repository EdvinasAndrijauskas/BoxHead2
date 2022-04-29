using UnityEngine;

namespace model.ammo
{
    public class Bullet : MonoBehaviour, IAmmoDamage
    {
        [SerializeField] private float ammoDamage;
        private void Update()
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            TakeDamage(col.collider,ammoDamage);
        }
    
        void OnBecameInvisible() {
            Destroy(gameObject);
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
            if (!col.gameObject.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
        
        }
    }
}
