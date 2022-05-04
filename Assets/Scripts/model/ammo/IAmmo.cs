using UnityEngine;

namespace model.ammo
{
    public interface IAmmo
    {
        void TakeDamage(Collider2D col, float damage);
    }
}