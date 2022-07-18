using System.Collections;
using System.Collections.Generic;
using Enemy.Monster;
using Manager;
using Player;
using UnityEngine;
// ReSharper disable All
#pragma warning disable CS0169

namespace Weapon
{
    public class RangeWeaponEnemy : MonoBehaviour
    {

        [SerializeField] private float m_moveSpeed;
        [SerializeField] private float m_damage;
        [SerializeField] private float m_lifeTime;
        [SerializeField] private bool m_isBulletBoss;
        private float m_currentLifeTime;


        // Start is called before the first frame update
        void Start()
        {
            m_currentLifeTime = m_lifeTime;
        }

        void Update()
        {
            Move();
            m_currentLifeTime -= Time.deltaTime;
            if (m_currentLifeTime <= 0)
            {
                
                SpawnManager.Instance.ReleaseEnemyBulelt(this , m_isBulletBoss);
                m_currentLifeTime = m_lifeTime;
            }
        }

        private void Move()
        {
            transform.Translate(Vector2.up * m_moveSpeed * Time.deltaTime);
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                PlayerController playerController;
                col.gameObject.TryGetComponent(out playerController);
                playerController.Hit(m_damage);
                SpawnManager.Instance.ReleaseEnemyBulelt(this , m_isBulletBoss);
            }
        }

        public void GetDamage(float damage)
        {
            m_damage = damage;
        }
    }
}
