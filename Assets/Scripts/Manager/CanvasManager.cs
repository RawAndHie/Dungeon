using System;
using System.Collections;
using System.Collections.Generic;
using SaveData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable All

namespace Manager
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_costHpTxt;
        [SerializeField] private TextMeshProUGUI m_costDamageTxt;
        [SerializeField] private TextMeshProUGUI m_healthTxt;
        [SerializeField] private TextMeshProUGUI m_damageTxt;
        [SerializeField] private TextMeshProUGUI m_coinTxt;
        [SerializeField] private TextMeshProUGUI m_expTxt;
        [SerializeField] private GameObject m_menuShop;
        [SerializeField] private GameObject m_canvasInfo;
        [SerializeField] private GameObject m_menuPause;
        [SerializeField] private GameObject m_gameOver;
        private static CanvasManager m_instance;

        public static CanvasManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<CanvasManager>();
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

            StartCanvas();
        }

        private void StartCanvas()
        {
            m_healthTxt.text = "Health : " + (int)PlayerData.GetHp();
            m_costHpTxt.text = "Cost : " + (int)PlayerData.GetCostHp();
            m_damageTxt.text = "Damage : " + (int)PlayerData.GetDamage();
            m_costDamageTxt.text = "Cost : " + (int)PlayerData.GetCostDamage();
            m_costDamageTxt.text = "" + PlayerData.GetCostDamage();
        }
        //shop
        public void ShowMenuShop()
        {
            m_coinTxt.text = "" + PlayerData.GetCoin();
            m_expTxt.text = "" + PlayerData.GetExp();
            GameManager.Instance.Pause();
            m_menuShop.gameObject.SetActive(true);
            m_canvasInfo.gameObject.SetActive(true);
        }
        public void BtnExitShop()
        {
            m_menuShop.gameObject.SetActive(false);
            m_canvasInfo.gameObject.SetActive(false);
            GameManager.Instance.Play();
        }
        public void UpgradeHpButton()
        {
            PlayerData.UpgradeHp();
            m_healthTxt.text = "Health : " + (int)PlayerData.GetHp();
            m_costHpTxt.text = "Cost : " + (int)PlayerData.GetCostHp();
            m_coinTxt.text = "" + PlayerData.GetCoin();
            SpawnManager.Instance.Player.UpdateCharacter();
        }

        public void UpgradeDamageButton()
        {
            PlayerData.UpgradeDamage();
            m_damageTxt.text = "Damage : " + (int)PlayerData.GetDamage();
            m_costDamageTxt.text = "Cost : " + (int)PlayerData.GetCostHp();
            m_coinTxt.text = "" + PlayerData.GetCoin();
            SpawnManager.Instance.Player.UpdateCharacter();
        }
        //pause menu
        public void PauseMenu()
        {
            GameManager.Instance.Pause();
            m_canvasInfo.gameObject.SetActive(true);
            m_menuPause.gameObject.SetActive(true);
        }
        public void BtnResume()
        {
            m_canvasInfo.gameObject.SetActive(false);
            m_menuPause.gameObject.SetActive(false);
            GameManager.Instance.Play();
        }
        
        // game over

        public void GameOver()
        {
            GameManager.Instance.Pause();
            m_gameOver.gameObject.SetActive(true);
        }

        public void BtnGameOver()
        {
            m_gameOver.gameObject.SetActive(false);
            SceneManager.LoadScene("GameMenu");
        }
        
        
    }
}