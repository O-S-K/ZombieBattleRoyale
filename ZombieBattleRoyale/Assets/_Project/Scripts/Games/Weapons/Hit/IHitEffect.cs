using UnityEngine;

namespace ZombieBattleRoyale
{
    public interface IHitEffect
    {
        void Hit(WeaponBase weapon, Vector3 position);
    }
}