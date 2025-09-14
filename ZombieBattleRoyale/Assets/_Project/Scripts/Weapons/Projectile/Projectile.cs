using System;
using System.Collections;
using System.Collections.Generic;
using OSK;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZombieBattleRoyale
{
    [AutoRegisterUpdate]
    public class Projectile : MonoBehaviour, IWeaponProjectile
    {
        protected IHitEffect hitEffect;

        [ReadOnly, ShowInInspector] protected WeaponBase weapon;
        [ReadOnly, ShowInInspector] protected Vector3 direction;

        protected float lifetime;
        protected float range;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            hitEffect = GetComponent<IHitEffect>();
        }

        public virtual void Create(WeaponBase weapon, Vector3 direction)
        {
            this.weapon = weapon;
            this.direction = direction;
            Debug.Log("Creating projectile");
        }

        public virtual void Destroyed()
        {
            hitEffect.Hit(weapon, transform.position);
            Main.Pool.Despawn(this);
        }

        protected virtual void CheckLifetime()
        {
            lifetime += Time.deltaTime;
            if (lifetime >= weapon.WeaponData.bulletData.lifetime || CheckRange())
            {
                Destroyed();
            }
        }

        protected bool CheckRange()
        {
            float distance = Vector3.Distance(transform.position, weapon.firePoint.position);
            return distance >= weapon.WeaponData.bulletData.range;
        }

        public virtual void FixedUpdate()
        {
            CheckLifetime();
        }
    }
}