using UnityEngine;
using UnityEngine.UI;

namespace ZombieBattleRoyale
{

    public class HealthBarShrinkUI : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        [SerializeField] private Image damagedBarImage;
        [SerializeField] private HealthController health;
         
        public float damageShrinkTimer = .6f;
        public float shrinkSpeed = 0.1f;
        private float damagedHealthShrinkTimer;

        private void Start()
        {
            damagedBarImage.fillAmount = barImage.fillAmount;
            health.OnDamage += OnDamaged;
            health.OnHeal += OnHealed;
        }

        private void OnDisable()
        {
            health.OnDamage -= OnDamaged;
            health.OnHeal -= OnHealed;
        }

        private void OnDestroy()
        {
            health.OnDamage -= OnDamaged;
            health.OnHeal -= OnHealed;
        }

        private void Update()
        {
            damagedHealthShrinkTimer -= Time.deltaTime;
            if (damagedHealthShrinkTimer < 0)
            {
                if (barImage.fillAmount < damagedBarImage.fillAmount)
                {
                    damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
                }
            }
        }

        private void OnHealed(int hp)
        {
            UpdateHeathBar(health.GetHealthPercent());
            damagedBarImage.fillAmount = barImage.fillAmount;
        }

        private void OnDamaged(int damage, DamagePopup.ETypeDamage isCrit)
        {
            damagedHealthShrinkTimer = damageShrinkTimer;
            UpdateHeathBar(health.GetHealthPercent());
        }

        private void UpdateHeathBar(float healthNormalized)
        {
            barImage.fillAmount = healthNormalized;
        }
    }

}
