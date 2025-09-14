using UnityEngine;
using UnityEngine.Serialization;

namespace ZombieBattleRoyale
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/Player/Stats", order = 1)]
    public class PlayerStats : ScriptableObject
    {
        public float SpeedWallking = 5f;
        public float SpeedRunning = 10f;
        
        public int HealthDefault = 100;
        public float StaminaDefault = 100f;

        // create constructor
        public PlayerStats()
        {
        }

        public PlayerStats(float speedWallking, int healthDefault, float stamina)
        {
            SpeedWallking = speedWallking;
            HealthDefault = healthDefault;
            StaminaDefault = stamina;
        }

        public PlayerStats Clone()
        {
            return ScriptableObject.CreateInstance<PlayerStats>();
        }
    }
}