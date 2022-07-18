using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyStatusBar : MonoBehaviour
    {
        private Vector3 m_localScale;

        private void Start()
        {
            m_localScale = transform.localScale;
        }

        private void Update()
        {
            m_localScale.x =EnemyController.m_hpBar;
            if (m_localScale.x >= 1)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                transform.localScale = m_localScale;
            }
        }
    }
}