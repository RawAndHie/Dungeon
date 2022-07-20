using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Item;
using Manager;
using SaveData;
using UnityEngine;
using Weapon;

// ReSharper disable All
#pragma warning disable CS0169

namespace Player
{
    public enum PlayerStatus
    {
        Alive,
        Dead
    }

    public class PlayerController : MonoBehaviour
    {
        private float m_health;
        private float m_startAttackSpeed = 0.3f;
        [SerializeField] private Transform m_firePoint;
        public PlayerStatus m_playerStatus;
        private bool m_isTriggerNpc;
        private float m_currentAttackSpeed = 0;
        private float m_currentHeal;

        private BoxCollider2D m_boxCollider2D;

        private void Awake()
        {
        }

        void Start()
        {
            m_boxCollider2D = GetComponent<BoxCollider2D>();
            UpdateCharacter();
            m_playerStatus = PlayerStatus.Alive;
            // sau này gọi từ spawn manager rồi load dữ liệu chỉ số nhân vật
        }

        // Update is called once per frame
        void Update()
        {
            if (m_playerStatus == PlayerStatus.Alive && GameManager.Instance.GetGameState() == GameState.Play)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Attack();
                }

                m_currentAttackSpeed -= Time.deltaTime;
            }

            PlayerInput();
        }

        public void UpdateCharacter()
        {
            m_currentHeal = PlayerData.GetHp();
            m_health = PlayerData.GetHp();
            UpdateStatus();
        }
        // cập nhật lại fill mỗi khi có thay đổi
        private void UpdateStatus()
        {
            GameManager.Instance.PlayerStatus((int) m_currentHeal, (int)m_health);
        }
        private void Attack()
        {
            if (m_currentAttackSpeed <= 0)
            {
                AttackByRate();
                m_currentAttackSpeed = m_startAttackSpeed;
            }
        }

        private void AttackByRate()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 dir = new Vector2(mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y);
            WeaponController weapon = SpawnManager.Instance.SpawnWeapon(m_firePoint.position);
            weapon.transform.up = dir;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Item"))
            {
                Debug.Log("va cham item");
            }

            if (col.gameObject.CompareTag("TileMap"))
            {
                Debug.Log("Va cham tilemap");
            }

            if (col.gameObject.CompareTag("LevelLoad"))
            {
                Debug.Log("va cham load level");
            }

            if (col.gameObject.CompareTag("NPC"))
            {
                m_isTriggerNpc = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                m_isTriggerNpc = false;
            }
        }

        public void Hit(float damage)
        {
            m_currentHeal -= damage;
            UpdateStatus();
            if (m_currentHeal <= 0)
            {
                Destroy(gameObject);
                m_playerStatus = PlayerStatus.Dead;
                CanvasManager.Instance.GameOver();
            }
        }

        private void PlayerInput()
        {
            if (m_isTriggerNpc == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Mo shop");
                    CanvasManager.Instance.ShowMenuShop();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CanvasManager.Instance.PauseMenu();
            }
        }

        
    }
}