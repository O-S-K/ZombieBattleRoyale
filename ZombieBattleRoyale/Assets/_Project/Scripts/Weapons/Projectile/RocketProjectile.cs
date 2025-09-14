using UnityEngine;

namespace ZombieBattleRoyale
{
    public class RocketProjectile : Projectile
    {
        public override void Create(WeaponBase weapon, Vector3 direction)
        {
            base.Create(weapon, direction);
            transform.position = weapon.firePoint.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}