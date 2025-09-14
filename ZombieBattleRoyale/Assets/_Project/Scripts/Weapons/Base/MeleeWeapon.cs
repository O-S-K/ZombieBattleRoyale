using UnityEngine;

namespace ZombieBattleRoyale
{
    public class MeleeWeapon : WeaponBase
    {
        public void Fire()
        {
            Debug.Log($"{weaponData.weaponName} vung dao gây {weaponData.damage} sát thương.");

            Collider[] hits = Physics.OverlapSphere(
                transform.position + transform.forward * weaponData.attackRange * 0.5f,
                weaponData.attackRange);

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Debug.Log($"Trúng {hit.name}, gây {weaponData.damage} damage.");
                    Rigidbody rb = hit.attachedRigidbody;
                    if (rb != null)
                        rb.AddForce(transform.forward * weaponData.knockBackForce, ForceMode.Impulse);
                }
            }
        }
    }
}