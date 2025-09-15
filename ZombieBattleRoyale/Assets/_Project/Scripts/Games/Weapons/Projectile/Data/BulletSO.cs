using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieBattleRoyale
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Game/Bullet", order = 1)]
    public class BulletSO : ScriptableObject
    {
        // Bullet
        public float speed = 20f;
        public float range = 50f;
        public int pierceCount = 0; // số lượng mục tiêu có thể xuyên qua
        public float explosionDamage = 20f; // sát thương nổ tính theo % percent của damage gốc
        public GameObject impactEffect; // hiệu ứng va chạm
        public GameObject muzzleFlashEffect; // hiệu ứng nòng súng
        public float lifetime = 5f; // thời gian tồn tại của đạn

        public BulletSO Clone()
        {
            return ScriptableObject.CreateInstance<BulletSO>();
        }
    }
}