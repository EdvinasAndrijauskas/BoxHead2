using System.Collections;
using SFX;
using UnityEngine;

namespace model.ammo
{
    public class Flame : MonoBehaviour, IAmmo
    {
        [SerializeField] private float ammoDamage;

        private Transform _firePoint;
        private Animator _animator;
        private static Flame _flame;
        private Vector3 _rotateFlame;
        private void Awake()
        {
            if (_flame == null)
            {
                _flame = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _firePoint = GameObject.FindGameObjectWithTag("FlameFirePoint").GetComponent<Transform>();

            StartCoroutine(PlayFlameSound());
            _rotateFlame = new Vector3(0,0,90 );
            transform.Rotate(_rotateFlame);
        }
        
        public IEnumerator PlayFlameSound()
        {
            GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().PlayOneShot("FlameStart");
            yield return new WaitForSeconds(0.5f);
            GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().Play("FlameMiddle");
        }
    
        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _firePoint.position , 100 * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_firePoint.rotation.eulerAngles  + _rotateFlame) ,
                350 * Time.deltaTime);

            bool endFlame = Input.GetButtonUp("Shoot") ||
                            Input.GetKeyDown(KeyCode.E) ||
                            Input.GetKeyDown(KeyCode.R) ||
                            Input.GetKeyDown(KeyCode.Q);
            if (endFlame)
            {
               EndFlame();
            }
        }

        public void EndFlame()
        {
            GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().Stop("FlameStart");
            GameObject.FindGameObjectWithTag("WeaponSound").GetComponent<AudioManager>().Stop("FlameMiddle");
            _animator.Play("Flamethrower End");
            _animator.fireEvents = false;
            Destroy(gameObject,0.25f);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            TakeDamage(col,ammoDamage);  
        }

        void OnBecameInvisible() {
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
