using UnityEngine;

namespace ZombieBattleRoyale
{
    public class MortarProjectile : Projectile
    {
        public override void Create(WeaponBase weapon, Vector3 direction)
        {
            base.Create(weapon, direction);
            Debug.Log($"Bắn súng cối, đạn rơi theo parabol gây sát thương AOE.");
        }
    }
}