using UnityEngine;

namespace ZombieBattleRoyale
{
    public class SingleShot : IWeaponFireMode
    {
        private WeaponBase weapon;
        private float timeCooldown;

        public SingleShot(WeaponBase weapon)
        {
            this.weapon = weapon;
            timeCooldown = weapon.WeaponData.fireRate;
        }

        public bool CanFire()
        {
            this.weapon = weapon;
            timeCooldown += UnityEngine.Time.deltaTime;
            Debug.Log(" SingleShot CanFire: " + timeCooldown + " / " + weapon.WeaponData.fireRate);
            if (timeCooldown >= weapon.WeaponData.fireRate && Input.GetMouseButtonDown(0))
            {
                timeCooldown = 0f;
                return true;
            }

            return false;
        }
    }
}