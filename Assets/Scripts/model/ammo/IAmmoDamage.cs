using UnityEngine;

namespace model.ammo
{
    public interface IAmmoDamage
    {
        void TakeDamage(Collider2D col, float damage);
    }
}