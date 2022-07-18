using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Manager;
using SaveData;
using UnityEngine;
// ReSharper disable All
#pragma warning disable CS0169

namespace Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private float m_damage;
        [SerializeField] private float m_lifeTime;
        
        private float m_currentLifeTime;

        private float m_playerDamage;

        private void Awake()
        {
            m_playerDamage = PlayerData.GetDamage();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_currentLifeTime = m_lifeTime;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            m_currentLifeTime -= Time.deltaTime;
            if (m_currentLifeTime <= 0)
            {
                SpawnManager.Instance.ReleaseWeapon(this);
                m_currentLifeTime = m_lifeTime;
            }
        }

        private void Move()
        {
            transform.Translate(Vector2.up * m_moveSpeed * Time.deltaTime);
        }
        

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                EnemyController enemyController;
                col.gameObject.TryGetComponent(out enemyController);
                enemyController.Hit(m_damage + m_playerDamage);
                SpawnManager.Instance.ReleaseWeapon(this);
            }
            
            if (col.gameObject.CompareTag("TileMap"))
            {
                Debug.Log("cham tilemap");
                EnemyController enemyController;
                col.gameObject.TryGetComponent(out enemyController);
                SpawnManager.Instance.ReleaseWeapon(this);
            }
        }
        
    }
}
