using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
// ReSharper disable All
#pragma warning disable CS0169

namespace Enemy.Monster
{
    public class RedMonter : EnemyCombat
    {
        [SerializeField] private float m_timeAttack;
        private float m_currentTimeAttack;
        private GameObject m_player;
        private EnemyController m_enemy;
       

        private void Start()
        {
            m_currentTimeAttack = m_timeAttack;
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_enemy = GetComponent<EnemyController>();
        }

        private void Update()
        {
            CheckPlayerPos();
        }

        private void CheckPlayerPos()
        {
            if (m_enemy.CheckAttackRadius() == true)
            {
                Attack();
            }
        }
        private void Attack()
        {
            if (m_currentTimeAttack <= 0)
            {
                RangeCombat(m_player.transform);
                m_currentTimeAttack = m_timeAttack;
            }
            else
            {
                m_currentTimeAttack -= Time.deltaTime;
            }
        }
    }
}
