using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using SaveData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Item
{
    public class CoinController : MonoBehaviour
    {
        private int m_value;

        private void Start()
        {
            m_value = Random.Range(1, 50);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                PlayerData.EarnCoin(m_value);
                Debug.Log(GameManager.Instance.PlayerModel.haveCoin);
            }
        }
    }
}
