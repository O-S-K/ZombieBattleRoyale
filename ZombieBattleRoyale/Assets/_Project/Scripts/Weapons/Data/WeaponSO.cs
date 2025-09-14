using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZombieBattleRoyale
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Game/Weapon", order = 1)]
    public class WeaponSO : ScriptableObject
    {
        [Header("Base Info")]
        public string weaponName;
        public WeaponCategory category = WeaponCategory.Ranged;

        [Header("Stats")]
        public float damage = 10f;
        public float fireRate = 1f;
        public int burstCount = 3;

        [Header("Ranged Modules")]
        public FireModeType fireModeType;
        public ProjectileType projectileType;
        public ReloadType reloadType;
        public float cooldownTime = 1f;
        
        public int maxAmmo = 30;
        public float spread = 0.1f;
        public float recoil = 1f; 

        [Header("Melee Settings")]
        public float attackRange = 2f;
        public float attackAngle = 45f; // góc chém
        public float knockBackForce = 5f;
        public BulletSO bulletData;
        
        // create clone method
        public WeaponSO Clone()
        {
            return ScriptableObject.CreateInstance<WeaponSO>();
        }
    
    }
}
