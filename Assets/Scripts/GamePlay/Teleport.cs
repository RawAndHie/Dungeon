using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace GamePlay
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Transform m_pointNextLevel;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                SpawnManager.Instance.Player.transform.position = m_pointNextLevel.position;
            }
        }

        public Transform GetPotal()
        {
            return m_pointNextLevel;
        }
    }

}