using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Player;
using UnityEngine;

// ReSharper disable All

namespace GamePlay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform m_lookAt;
        private Transform m_player;
        private float m_boundX = 0.15f;
        private float m_boundY = 0.05f;

        private void Start()
        {
            m_player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            if (SpawnManager.Instance.Player.m_playerStatus == PlayerStatus.Alive)
            {
                Vector3 delta = Vector3.zero;
                float deltaX = m_player.position.x - transform.position.x;
                float deltaY = m_player.position.y - transform.position.y;

                if (deltaX > m_boundX || deltaX < m_boundX)
                {
                    if (transform.position.x < m_player.position.x)
                    {
                        delta.x = deltaX - m_boundX;
                    }
                    else
                    {
                        delta.x = deltaX + m_boundX;
                    }
                }

                if (deltaY > m_boundY || deltaY < m_boundY)
                {
                    if (transform.position.y < m_player.position.y)
                    {
                        delta.y = deltaY - m_boundY;
                    }
                    else
                    {
                        delta.y = deltaY + m_boundY;
                    }
                }

                transform.position += new Vector3(delta.x, delta.y, 0);
            }
        }
    }
}