using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieBattleRoyale
{
    public class HitEffectNone : MonoBehaviour, IHitEffect
    {
        public void Hit(WeaponBase weapon, Vector3 position)
        {
            // No effect
        }
    }
}
