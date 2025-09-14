using System.Collections;
using System.Collections.Generic;
using OSK;
using UnityEngine;

namespace ZombieBattleRoyale
{
    [AutoRegisterUpdate]
    public class PlayerWeapon : MonoBehaviour, IUpdate
    {
        public WeaponBase currentWeapon;
        public WeaponBase CurrentWeapon => currentWeapon;

        public void SetCurrentWeapon(WeaponBase weapon)
        {
            currentWeapon?.UnEquip();
            currentWeapon = weapon;
            currentWeapon.Equip();
        }
        
        public void ClearCurrentWeapon()
        {
            currentWeapon = null;
        }

        public void Tick(float deltaTime)
        {
            if (currentWeapon != null)
            {
                currentWeapon.Tick(deltaTime);
            }
        }
    }
}
