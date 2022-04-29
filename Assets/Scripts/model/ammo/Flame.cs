using UnityEngine;

namespace model.ammo
{
    public class Flame : MonoBehaviour, IAmmoDamage
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
            _rotateFlame = new Vector3(0,0,90 );
            transform.Rotate(_rotateFlame);

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
            if (col.tag.Equals("Enemy"))
            {
                col.gameObject.GetComponent<ZombieHealth>().Damage(ammoDamage);
            
                if (col.gameObject.GetComponent<ZombieHealth>().CurrentHealth.Equals(0))
                {
                    Destroy(col.gameObject);
                }
            }    
        }
    }
}
