using Sirenix.OdinInspector;
using UnityEngine;

namespace ZombieBattleRoyale
{ 
    public class Projectile : MonoBehaviour, IWeaponProjectile
    {
        [ReadOnly, ShowInInspector]
        protected IHitEffect hitEffect;

        [ReadOnly, ShowInInspector] protected WeaponBase weapon;
        [ReadOnly, ShowInInspector] protected Vector3 direction;

        [ReadOnly, ShowInInspector]
        protected float lifetime;
        protected float range;

        protected void Awake()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            hitEffect = GetComponent<IHitEffect>();
        }

        public virtual void Create(WeaponBase weapon, Vector3 direction)
        {
            this.weapon = weapon;
            this.direction = direction;
            range = weapon.WeaponData.bulletData.range;
            Debug.Log("Creating projectile"); 
        }

        public virtual void Destroyed()
        {
            //hitEffect.Hit(weapon, transform.position);
           Destroy(gameObject);
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
            return distance >= range;
        }
 
        public virtual void FixedUpdate()
        {
            CheckLifetime();
        }
    }
}