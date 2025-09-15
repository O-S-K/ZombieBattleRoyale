using UnityEngine;

namespace ZombieBattleRoyale
{
    public class RangedWeapon : WeaponBase
    {
        [SerializeReference] protected IWeaponReload reloadModule;
        [SerializeReference] protected IWeaponFireMode fireMode;
        [SerializeField] protected Projectile projectile;
         protected int currentAmmo;

        public int CurrentAmmo
        {
            get => currentAmmo;
            set => currentAmmo = value;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void SetupData()
        {
            base.SetupData();
            SetupWeapon();
        }

        protected void SetupWeapon()
        {
            // --- RANGED SETUP ---
            currentAmmo = weaponDataDefault.maxAmmo;

            // FireMode
            switch (weaponDataDefault.fireModeType)
            {
                case FireModeType.Single:
                    fireMode = new SingleShot(this);
                    break;
                case FireModeType.Auto:
                    fireMode = new AutoFire(this);
                    break;
                case FireModeType.Burst:
                    fireMode = new BurstFire(this);
                    break;
            }
 

            // Reload
            switch (weaponDataDefault.reloadType)
            {
                case ReloadType.Cooldown:
                    reloadModule = new CooldownReload(weaponDataDefault.cooldownTime);
                    break;
            }
        }


        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            
            Fire();
            if(Input.GetKeyDown(KeyCode.R))
            {
                if(IsReloading) 
                    return;
                if(currentAmmo >= weaponDataDefault.maxAmmo)
                {
                    Debug.Log($"{weaponDataDefault.weaponName} đã đầy đạn!");
                    return;
                }
                Reload();
            }
        }

        public virtual void Fire()
        {
            // --- RANGED ---
            if (currentAmmo <= 0 && !IsReloading)
            {
                Reload();
                Debug.Log($"{weaponDataDefault.weaponName} hết đạn!");
                return;
            }
            
            if (fireMode.CanFire())
            {
                ShootProjectile();
                currentAmmo--;
            }
        } 
        
        protected virtual void ShootProjectile()
        {
            if (projectile != null)
            {
                Projectile newProjectile = Instantiate(projectile, firePoint);
                newProjectile.Create(this, firePoint.forward);
                Debug.Log($"{weaponDataDefault.weaponName} bắn đạn. Đạn còn lại: {currentAmmo}/{weaponDataDefault.maxAmmo}");
            }
            else
            {
                Debug.LogWarning("Projectile is not assigned.");
            }
        }
        

        public virtual void Reload()
        {
            IsReloading = true;
            reloadModule.Reload(this);
        }
    }
}