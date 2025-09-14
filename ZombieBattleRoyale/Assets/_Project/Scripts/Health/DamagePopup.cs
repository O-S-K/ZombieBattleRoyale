using OSK;
using UnityEngine;
using TMPro;

namespace ZombieBattleRoyale
{
    public class DamagePopup : MonoBehaviour
    {
        public enum ETypeDamage
        {
            None,
            Nomal,
            CriticalHit,
            BuffHeal
        }
        public TextMeshPro textMesh; 

        private float disappearTimerMax = 0.25f;
        private float disappearTimer;
        private int sortingOrder;

        private Color textColor;
        private Vector3 moveVector;

        public void Setup(int damageAmount, ETypeDamage typeDamage)
        {
            string value = damageAmount > 0 ? $"+{damageAmount}" : $"{damageAmount}";
            textMesh.SetText(value);

            switch (typeDamage)
            {
                case ETypeDamage.Nomal:
                    textMesh.fontSize = 34;
                    textColor = Color.white;
                    break;
                case ETypeDamage.CriticalHit:
                    textMesh.fontSize = 40;
                    textColor = Color.yellow;
                    break;
                case ETypeDamage.BuffHeal:
                    textMesh.fontSize = 36;
                    textColor = Color.green;
                    break;
            }
          
            textMesh.color = textColor;
            disappearTimer = disappearTimerMax;

            sortingOrder++;
            textMesh.sortingOrder = 1000 + sortingOrder;
            moveVector = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1)) * 2;
            transform.localScale = Vector3.one; 
        }

        private void Update()
        {
            transform.position += moveVector * Time.deltaTime;
            moveVector -= moveVector * (Random.Range(-10, 10f) * Time.deltaTime);

            if (disappearTimer > disappearTimerMax * .5f)
            {
                // First half of the popup lifetime
                float increaseScaleAmount = 1f;
                transform.localScale += Vector3.one * (increaseScaleAmount * Time.deltaTime);
            }
            else
            {
                // Second half of the popup lifetime
                float decreaseScaleAmount = 1f;
                transform.localScale -= Vector3.one * (decreaseScaleAmount * Time.deltaTime);
            }

            disappearTimer -= Time.deltaTime;
            if (disappearTimer < 0)
            {
                // Start disappearing
                float disappearSpeed = 5f;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a <= 0.1)
                {
                    Main.Pool.Despawn(this);
                }
            }
        }
    }
}

