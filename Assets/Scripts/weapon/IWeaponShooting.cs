namespace weapon
{
    public interface IWeaponShooting
    {
        void DirectShooting(string ammo);
        void SpreadShooting(string ammo, float numberOfProjectiles, float spreadAngle);
        void FlamethrowerShooting(string ammo);
        void LaserShooting(string ammo);
    }
}