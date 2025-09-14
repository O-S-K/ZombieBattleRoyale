using OSK;
using UnityEngine;

namespace ZombieBattleRoyale
{
    [AutoRegisterUpdate]
    public class WeaponBase : MonoBehaviour, IWeaponEquip, IUpdate
    {
        [SerializeField] protected WeaponSO weaponDataDefault;
        protected WeaponSO weaponData;
        public WeaponSO WeaponData => weaponData;
        public bool IsEquipped { get; set; }
        public bool IsReloading { get; set; }

        public Transform firePoint;

        private void Awake()
        {
            SetupData();
            Initialize();
        } 

        public virtual void SetupData()
        {
            SetData(weaponDataDefault);
        }

        public virtual void Initialize()
        {
            IsReloading = false;
        }

        protected void SetData(WeaponSO weaponDataUpdate)
        {
            weaponData = weaponDataUpdate;
        }

        public void Equip()
        {
        }
 

        public void UnEquip()
        {
        }

        public virtual void Tick(float deltaTime)
        {
        }
    }
}