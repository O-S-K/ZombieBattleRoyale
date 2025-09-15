using Sirenix.OdinInspector;
using UnityEngine;

namespace ZombieBattleRoyale
{
	public class LifeController : MonoBehaviour, IDamageable 
    {
        public System.Action<int, DamagePopup.ETypeDamage> OnDamage;
        public System.Action<int> OnHeal;
        public System.Action OnDie;

        [ReadOnly, SerializeField]
        public float MaxHealth;
        
        [ReadOnly, SerializeField]
        public float CurrentHealth;
        
         

        public virtual void SetupHP(float startingHP)
        {
            MaxHealth = startingHP;
            CurrentHealth = startingHP; 
        }
        
        public virtual void SetMaxHP(float maxHp)
        {
            MaxHealth = maxHp;
            CurrentHealth = MaxHealth;
            OnHeal?.Invoke((int)CurrentHealth);
        }
        
        public float GetHealthPercent() => (float)CurrentHealth / MaxHealth;

        [Button]
        public virtual bool ChangeHP(float value, Vector3 position)
        {
            if (value == 0) return false;
            
            CurrentHealth += value;
            CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
            CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

            if (value > 0) OnHeal?.Invoke((int)value);
            else OnDamage?.Invoke((int)value, DamagePopup.ETypeDamage.Nomal);

            if (CurrentHealth < 0.01f) Die();
            return true;
        }

        public virtual void Die() => OnDie?.Invoke();
    }
}
