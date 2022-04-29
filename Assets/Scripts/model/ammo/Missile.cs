using UnityEngine;

namespace model.ammo
{
    public class Missile : MonoBehaviour, IAmmoDamage
    {
        public GameObject explosion;
        [SerializeField] private float ammoDamage;
        private float _explosionRadius = 20;

        private void Update()
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.collider.tag.Equals("Soldier"))
            {
                Destroy(gameObject);
                OnDestroy();
            }
        }
    
        private void OnDestroy()
        {
            GameObject missileExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        
            Collider2D[] colliders = Physics2D.OverlapCircleAll(missileExplosion.transform.position, _explosionRadius);
            foreach (Collider2D collider in colliders)
            {
               TakeDamage(collider, ammoDamage);
            
            }
        
            Destroy(missileExplosion,0.5f);
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

        }
    }
}
