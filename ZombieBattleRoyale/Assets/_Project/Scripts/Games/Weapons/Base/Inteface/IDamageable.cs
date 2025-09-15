using UnityEngine;

namespace ZombieBattleRoyale
{
    public interface IDamageable
    {
        bool ChangeHP(float damage, Vector3 position);
    } 
}
