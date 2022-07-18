using System;
using System.Collections;
using System.Collections.Generic;
using SaveData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable All
#pragma warning disable CS0169

namespace Manager
{
    public enum GameState
    {
        Play, Pause
    }
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image m_fillHealth;
        [SerializeField] private TextMeshProUGUI m_healthTxt;
        private static GameManager m_instance;
        private PlayerModel m_playerModel;
        private GameState m_gameState;
        public PlayerModel PlayerModel => m_playerModel;
        public static GameManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<GameManager>();
                }

                return m_instance;
            }
        }

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else if (m_instance != this)
            {
                Destroy(gameObject);
            }
            AudioManager.Instance.BackgroundMusic();
            SpawnManager.Instance.StartGame();
            GetDataPlayer();
        }

        public void GetDataPlayer()
        {
            m_playerModel = PlayerData.GetAll();
        }
        public void PlayerStatus(float currentHealt, float maxHealth)
        {
            m_fillHealth.fillAmount = currentHealt * 1f / maxHealth;
            m_healthTxt.text = currentHealt + "/" + maxHealth;
        }

        public GameState GetGameState()
        {
            return m_gameState;
        }
        public void Play()
        {
            SetGameState(GameState.Play);
        }

        public void Pause()
        {
            SetGameState(GameState.Pause);
        }

        private void SetGameState(GameState gameState)
        {
            if (gameState == GameState.Pause)
            {
                Time.timeScale = 0;
                m_gameState = GameState.Pause;
            }

            if (gameState == GameState.Play)
            {
                Time.timeScale = 1;
                m_gameState = GameState.Play;
            }
        }
    }
}