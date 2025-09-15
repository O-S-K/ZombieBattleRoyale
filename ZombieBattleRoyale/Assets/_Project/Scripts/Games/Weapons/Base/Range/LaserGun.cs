using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieBattleRoyale
{
    public class LaserGun : RangedWeapon
    {
        public override void Fire()
        {
            if (fireMode.CanFire())
            {
                // Raycast laser
                Ray ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    Debug.Log($"Laser trúng {hit.collider.name}, gây {weaponData.damage} DPS.");
                }
            }
        }
    }
}
