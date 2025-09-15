using UnityEngine;

namespace ZombieBattleRoyale
{
    public class HealthController : LifeController
    {
        public HealthBarShrinkTransform barShrink;
        public float healthChangeDelay = .5f;

        public System.Action OnInvincibilityEnd;
        protected float _timeSinceLastChange = float.MaxValue;


        public override void SetupHP(float startingHP)
        {
            base.SetupHP(startingHP);
            barShrink.Initialize(this);
        }

        public override void SetMaxHP(float maxHp)
        {
            base.SetMaxHP(maxHp); 
        }

        private void Update()
        {
            if (_timeSinceLastChange < healthChangeDelay)
            {
                _timeSinceLastChange += Time.deltaTime;
                if (_timeSinceLastChange >= healthChangeDelay)
                {
                    OnInvincibilityEnd?.Invoke();
                }
            }
        }

        public override bool ChangeHP(float value, Vector3 position)
        {
            if (value == 0 || _timeSinceLastChange < healthChangeDelay)
            {
                return false;
            }
            return base.ChangeHP(value, position);
        }
    }
}