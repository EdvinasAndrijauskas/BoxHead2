using SFX;
using UnityEngine;

namespace model.ammo
{
    public class Grenade : MonoBehaviour, IAmmo
    {
        public GameObject explosion;
        private float _countdown;
        private float _explosionRadius = 5;

        [SerializeField] private float ammoDamage;

        private void Start()
        {
            _countdown = Random.Range(1f,2f);
        }

        private void Update()
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            transform.Rotate(new Vector3(0, 0, 360 * Time.deltaTime));
            Stop(0.75f);

            _countdown -= Time.deltaTime;
            if (_countdown <= 0)
            {
                Explode();
                Destroy(gameObject);
            }
        }

        private void Stop(float stop)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>(),stop);
            Destroy(gameObject.GetComponent<BoxCollider2D>(),stop);
        }

        private void Explode()
        {
            GameObject explosionEffect = Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().PlayOneShot("EMP_Grenade");

            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionEffect.transform.position, _explosionRadius);
            foreach (Collider2D collider in colliders)
            {
                TakeDamage(collider,ammoDamage);
            
            }
            Destroy(explosionEffect,0.5f);
            Destroy(gameObject);
        }

        void OnBecameInvisible() {
            Stop(0.0f);
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
