using UnityEngine;

namespace ZombieBattleRoyale
{
    public class BurstFire : IWeaponFireMode
    {
        private int burstCount;
        private int fired;
        private WeaponBase weapon;

        public BurstFire(WeaponBase weapon)
        {
            this.weapon = weapon;
            this.burstCount = weapon.WeaponData.burstCount;
        }
        
        public bool CanFire()
        {
            Debug.Log(" BurstFire CanFire: " + fired + " / " + burstCount);
            if (fired < burstCount && Input.GetMouseButton(0))
            {
                fired++;
                return true;
            }
            fired = 0; 
            return false;
        } 
    }
}