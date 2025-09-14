using System;
using UnityEngine;

namespace ZombieBattleRoyale
{
    public class BulletProjectile : Projectile
    {
        private IMoveVelocity moveVelocity;
        
        private void Awake()
        {
            moveVelocity = GetComponent<IMoveVelocity>();
        }

        public override void Create(WeaponBase weapon, Vector3 direction)
        {
            base.Create(weapon, direction);
            transform.position = weapon.firePoint.position; 
            transform.parent = null;
            transform.rotation = Quaternion.LookRotation(direction);
            moveVelocity.SetUseAcceleration(false);
            moveVelocity.SetSpeed(weapon.WeaponData.bulletData.speed);
         
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            moveVelocity.SetVelocity(direction);
        }
    }
}