using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieBattleRoyale
{
    public interface IWeaponProjectile
    {
        void Initialize();
        void Create(WeaponBase weapon, Vector3 direction);
        void Destroyed();
    }
}
