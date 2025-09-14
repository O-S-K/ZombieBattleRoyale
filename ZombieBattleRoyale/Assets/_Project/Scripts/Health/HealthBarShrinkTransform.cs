using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace ZombieBattleRoyale
{
    public class HealthBarShrinkTransform : MonoBehaviour
    {
        public bool isShrinkFake = true;
        public float damageShrinkTimer = .6f;
        public float shrinkSpeed = 0.1f;

        public bool isPlayer = false;
        public bool isHideBar = true;

        public TextMeshPro textStats;
        private Transform _barLv;


        private HealthController _health;
        [SerializeField] private Transform _bar;
        [SerializeField] private Transform _damagedBar;
        [SerializeField] private Transform _pivotBar;

        private float _xScale;
        private float _damagedHealthShrinkTimer;

        private bool _isShowBar;
        private float _timeCheckHideBar;

        public void Initialize(HealthController health)
        {
            _health = health;
            _health.OnDamage += OnDamaged;
            _health.OnHeal += OnHealed;
            
            _xScale = 1;
            _timeCheckHideBar = 5;

            if (isHideBar)
            {
                _isShowBar = false;
                HideBar();
            }

            _damagedBar.localScale = Vector3.one;
            _bar.localScale = Vector3.one; 
            if(textStats) textStats.text = ((int)_health.CurrentHealth).ToString();
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.OnDamage -= OnDamaged;
                _health.OnHeal -= OnHealed;
            }
        }

        private void Update()
        {
            _damagedHealthShrinkTimer -= Time.deltaTime;
            if (_damagedHealthShrinkTimer < 0)
            {
                if (_bar.localScale.x < _damagedBar.localScale.x)
                {
                    _xScale = Mathf.Lerp(_damagedBar.localScale.x, _bar.localScale.x, shrinkSpeed * Time.deltaTime);
                    _damagedBar.localScale = new Vector3(_xScale, 1, 1);
                }
            }

            if (isHideBar)
            {
                if (_isShowBar)
                {
                    _timeCheckHideBar -= Time.deltaTime;
                    if (_timeCheckHideBar < 0)
                    {
                        _timeCheckHideBar = 5;
                        _isShowBar = false;
                        HideBar();
                    }

                    ShowBar();
                }
                else
                {
                    HideBar();
                }
            }
        }


        protected void OnHealed(int hp)
        {
            _isShowBar = true;
            _timeCheckHideBar = 5;
            UpdateHeathBar(_health.GetHealthPercent());
            _damagedBar.localScale = new Vector3(_bar.localScale.x, 1, 1);
        }

        protected void OnDamaged(int damage, DamagePopup.ETypeDamage typeDamage)
        {
            _isShowBar = true;
            _timeCheckHideBar = 5;
            _damagedHealthShrinkTimer = damageShrinkTimer;
            UpdateHeathBar(_health.GetHealthPercent());
        }

        private void UpdateHeathBar(float healthNormalized)
        {
            if (healthNormalized < 0) healthNormalized = 0;
            _bar.localScale = new Vector3(healthNormalized, 1, 1);
            if (isPlayer) textStats.text = ((int)_health.CurrentHealth).ToString();
        }

        private void ShowBar()
        {
            if (!_pivotBar.gameObject.activeInHierarchy)
            {
                _pivotBar.gameObject.SetActive(true);
            }
        }

        private void HideBar()
        {
            if (_pivotBar.gameObject.activeInHierarchy)
            {
                _pivotBar.gameObject.SetActive(false);
            }
        }
    }
}