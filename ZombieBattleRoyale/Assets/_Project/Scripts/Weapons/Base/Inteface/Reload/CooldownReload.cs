using System.Collections;
using UnityEngine;

namespace ZombieBattleRoyale
{
    public class CooldownReload : IWeaponReload
    {
        private float cooldownTime;
        public CooldownReload(float time) { cooldownTime = time; }
        public void Reload(RangedWeapon weapon)
        {
            weapon.StartCoroutine(ReloadCoroutine(weapon));
        }
        
        private IEnumerator ReloadCoroutine(RangedWeapon weapon)
        {
            weapon.IsReloading = true;
            yield return new WaitForSeconds(cooldownTime);
            weapon.CurrentAmmo = weapon.WeaponData.maxAmmo;
            weapon.IsReloading = false;
        }
    }
}