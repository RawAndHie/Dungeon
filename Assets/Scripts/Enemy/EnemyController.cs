using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Player;
using SaveData;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

#pragma warning disable CS0414
#pragma warning disable CS0169

// ReSharper disable All

namespace Enemy
{
    public enum EnemyStatus
    {
        Alive,
        Dead
    }

    public class EnemyController : MonoBehaviour
    {
        [Header("Radius")] [SerializeField] private float m_radiusFollow;
        [SerializeField] private float m_radiusStop; // trong vùng stop thì chỉ nhìn player , vùng stop > vùng tấn công 
        [SerializeField] private float m_radiusAttack;
        [Header("Setting")] [SerializeField] private bool m_isReturnStartPoint;
        [SerializeField] private bool m_alwayFollow;
        [Header("Index")] [SerializeField] private float m_moveSpeed;
        [SerializeField] private EnemyModel m_enemyModel;
        [SerializeField] private int m_level;
        private EnemyCombat m_enemyCombat;
        private float m_hp;
        private float m_enemyDamage;
        private Transform m_playerPos;
        private bool m_isMove;
        private bool m_isAttack;
        private Vector3 m_startPosition;
        public static float m_hpBar;
        private float m_currentHp;
        private EnemyStatus m_status;

        // Start is called before the first frame update
        void Start()
        {
            m_playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            m_startPosition = transform.position;
            GetInfoEnemy();
            m_currentHp = m_hp;
            m_status = EnemyStatus.Alive;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void GetInfoEnemy()
        {
            m_hp = m_enemyModel.GetHealth(m_level);
            m_enemyDamage = m_enemyModel.GetDamage(m_level);
        }
        public bool CheckAttackRadius()
        {
            if (m_status == EnemyStatus.Alive && SpawnManager.Instance.Player.m_playerStatus == PlayerStatus.Alive)
            {
                if (Vector2.Distance(transform.position, m_playerPos.transform.position) < m_radiusAttack)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Move()
        {
            if (m_status == EnemyStatus.Alive && SpawnManager.Instance.Player.m_playerStatus == PlayerStatus.Alive)
            {
                if (Vector2.Distance(transform.position, m_playerPos.position) < m_radiusStop)
                {
                    m_isMove = false;
                    RolateTarget(true);
                }
                else if (Vector2.Distance(transform.position, m_playerPos.position) < m_radiusFollow)
                {
                    m_isMove = true;
                    m_isAttack = false;
                    transform.position = Vector2.MoveTowards(transform.position, m_playerPos.position,
                        m_moveSpeed * Time.deltaTime);
                    RolateTarget(true);
                }
                else if (m_isAttack)
                {
                    transform.position = Vector2.MoveTowards(transform.position, m_playerPos.position,
                        m_moveSpeed * Time.deltaTime);
                }
                else if (m_isReturnStartPoint &&
                         Vector2.Distance(transform.position, m_playerPos.position) > m_radiusFollow &&
                         Vector2.Distance(transform.position, m_playerPos.position) > 0)
                {
                    m_isMove = true;
                    transform.position =
                        Vector2.MoveTowards(transform.position, m_startPosition, m_moveSpeed * Time.deltaTime);
                    RolateTarget(false);
                }
                else if (m_alwayFollow && m_isMove)
                {
                    transform.position = Vector2.MoveTowards(transform.position, m_playerPos.position,
                        m_moveSpeed * Time.deltaTime);
                    RolateTarget(true);
                }
                else
                {
                    // khi đứng yên
                    m_isMove = false;
                }
            }
            else
            {
                return;
            }
        }

        private void RolateTarget(bool player)
        {
            if (player)
            {
                if (m_playerPos.position.x - transform.position.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (m_playerPos.position.x - transform.position.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                if (m_startPosition.x - transform.position.x > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (m_startPosition.x - transform.position.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("vacham player");
                // PlayerController playerController;
                // col.gameObject.TryGetComponent(out playerController);
                // playerController.Hit(m_enemyDamage);
            }
        }

        public void Hit(float damage)
        {
            m_currentHp -= damage;
            m_hpBar = m_currentHp / m_hp;
            if (m_currentHp <= 0)
            {
                Destroy(gameObject);
                m_status = EnemyStatus.Dead;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, m_radiusFollow);
            Gizmos.DrawWireSphere(transform.position, m_radiusAttack);
            Gizmos.DrawWireSphere(transform.position, m_radiusStop);
        }

        public float GetDamage()
        {
            return m_enemyDamage;
        }
    }

    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private Transform m_firePoint;
        [SerializeField] private bool m_isBoss;
        private EnemyController m_enemy;

        private void Start()
        {
            m_enemy = GetComponent<EnemyController>();
        }

        private void MeleeCombat()
        {
        }

        public void RangeCombat(Transform target)
        {
            if (m_isBoss == false)
            {
                RangeWeaponEnemy weaponController = SpawnManager.Instance.SpawnEnemyBullet(m_firePoint.position);
                weaponController.transform.position = m_firePoint.position;
                var dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
                weaponController.transform.up = dir;
            }
            else
            {
                RangeWeaponEnemy weaponController = SpawnManager.Instance.SpawnBossBullet(m_firePoint.position);
                weaponController.transform.position = m_firePoint.position;
                var dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
                weaponController.transform.up = dir;
            }
        }
        
        public void RangeBossCombat(Transform target)
        {
            RangeWeaponEnemy weaponController = SpawnManager.Instance.SpawnBossBullet(m_firePoint.position);
            weaponController.transform.position = m_firePoint.position;
            var dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            weaponController.transform.up = dir;
        }
        
    }
}