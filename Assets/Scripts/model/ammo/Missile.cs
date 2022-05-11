using SFX;
using UnityEngine;

namespace model.ammo
{
    public class Missile : MonoBehaviour, IAmmo
    {
        public GameObject explosion;
        [SerializeField] private float ammoDamage;
        private float _explosionRadius = 15;

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
            GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().Play("Missile");

            Collider2D[] colliders = Physics2D.OverlapCircleAll(missileExplosion.transform.position, _explosionRadius);
            foreach (Collider2D collider in colliders)
            {
                TakeDamage(collider, ammoDamage);
            }

            Destroy(missileExplosion, 0.5f);
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        public void TakeDamage(Collider2D col, float damage)
        {
            if (col.tag.Equals("Zombie"))
            {
                col.gameObject.GetComponent<ZombieHealth>().Damage(damage);
                if (col.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
                {
                    Destroy(col.gameObject);
                }
            }

            if (col.tag.Equals("Wizard"))
            {
                col.gameObject.GetComponent<Wizardhealth>().Damage(damage);
                if (col.gameObject.GetComponent<Wizardhealth>().CurrentHealth.Equals(0))
                {
                    Destroy(col.gameObject);
                }
            }
        }
    }
}