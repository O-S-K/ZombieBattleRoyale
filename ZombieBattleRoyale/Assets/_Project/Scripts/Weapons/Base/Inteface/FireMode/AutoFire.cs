using UnityEngine;

namespace ZombieBattleRoyale
{
    public class AutoFire : IWeaponFireMode
    {
        private float fireRate;
        private WeaponBase weapon;

        public AutoFire(WeaponBase weapon)
        {
            this.weapon = weapon;
            fireRate = weapon.WeaponData.fireRate;
        }

        public bool CanFire()
        {
            fireRate += Time.deltaTime;
            //Debug.Log(" AutoFire CanFire: " + fireRate + " / " + weapon.WeaponData.fireRate);
            if (fireRate >= weapon.WeaponData.fireRate && Input.GetMouseButton(0))
            {
                fireRate = 0f;
                return true;
            }
            return false;
        }
    }
}