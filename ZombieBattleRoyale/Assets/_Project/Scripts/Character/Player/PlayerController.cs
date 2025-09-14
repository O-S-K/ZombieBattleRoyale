using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieBattleRoyale
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private HealthController healthController;
        
        public PlayerStats PlayerStats => playerStats;
        private IMoveVelocity moveVelocity;
        private bool isWalking;

        private void Awake()
        {
            SetupData();
            Initialize();
        }
        
        public void Initialize()
        {
            isWalking = false; 
        }
        
        public void SetupData()
        { 
            healthController.SetupHP(PlayerStats.HealthDefault);
        }

        private void Update()
        {
            Movement();
            Rotation();
        }

        private void Movement()
        {
            if (moveVelocity == null) moveVelocity = GetComponent<IMoveVelocity>();

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            float speedMultiplier = isWalking ? playerStats.SpeedWallking : playerStats.SpeedRunning;
            isWalking = Input.GetKey(KeyCode.LeftShift);

            Vector3 moveDir = new Vector3(h, 0f, v).normalized;
            moveVelocity.SetSpeed(speedMultiplier);
            moveVelocity.SetVelocity(moveDir);
        }

        private void Rotation()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // khoảng cách từ camera tới mặt phẳng muốn chiếu (tùy chỉnh)

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 lookDir = (worldPos - transform.position).normalized;

            lookDir.y = 0f; // giữ cho nhân vật không ngẩng/úp
            if (lookDir.sqrMagnitude > 0.001f)
            {
                float targetAngle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            }
        }
    }
}