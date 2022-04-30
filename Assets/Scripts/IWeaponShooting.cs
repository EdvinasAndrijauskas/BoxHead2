namespace DefaultNamespace
{
    public interface IWeaponShooting
    {
        void DirectShooting(string ammo);
        void SpreadShooting(string ammo, float numberOfProjectiles, float spreadAngle);
        void FlamethrowerShooting();
        void LaserShooting(string ammo);
    }
}