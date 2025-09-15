using UnityEngine;

namespace ZombieBattleRoyale
{
    public class PlayerWeapon : MonoBehaviour
    {
        public WeaponBase currentWeapon;
        public WeaponBase CurrentWeapon => currentWeapon;
        
        public void EquipWeapon(WeaponBase newWeapon)
        {
            if (currentWeapon != null)
                currentWeapon.UnEquip();

            currentWeapon = newWeapon;
            currentWeapon?.Equip();
        }
        
        public void UnEquipWeapon()
        {
            if (currentWeapon != null)
            {
                currentWeapon.UnEquip();
                currentWeapon = null;
            }
        }
        
        public void ClearCurrentWeapon()
        {
            currentWeapon = null;
        }

        private void Update()
        {
            if (currentWeapon != null)
            {
                currentWeapon.Tick(Time.deltaTime);
            }
        }
    }
}
