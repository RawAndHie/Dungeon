using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Weapon;

// ReSharper disable All
#pragma warning disable CS0649
#pragma warning disable CS0169

namespace Manager
{
    [Serializable]
    public class WeaponPooling
    {
        public WeaponController weaponPrefab;
        public List<WeaponController> activeObj;
        public List<WeaponController> inactiveObj;
        private Vector3 m_position;

        public WeaponController SpawnObj(Vector3 position, Transform parent)
        {
            if (inactiveObj.Count == 0)
            {
                WeaponController obj = GameObject.Instantiate(weaponPrefab, parent);
                obj.transform.position = position;
                m_position = position;
                activeObj.Add(obj);
                return obj;
            }
            else
            {
                WeaponController obj = inactiveObj[0];
                obj.transform.position = position;
                obj.gameObject.SetActive(true);
                obj.transform.SetParent(parent);
                activeObj.Add(obj);
                inactiveObj.RemoveAt(0);
                return obj;
            }
        }

        public void ReleasseObj(WeaponController weapon)
        {
            if (activeObj.Contains(weapon))
            {
                weapon.gameObject.SetActive(false);
                activeObj.Remove(weapon);
                inactiveObj.Add(weapon);
            }
        }

        public void Clear()
        {
            while (activeObj.Count > 0)
            {
                WeaponController obj = activeObj[0];
                obj.gameObject.SetActive(false);
                activeObj.RemoveAt(0);
                inactiveObj.Add(obj);
            }
        }
    }

    [Serializable]
    public class EnemyWeaponPooling
    {
        public RangeWeaponEnemy weaponPrefab;
        public List<RangeWeaponEnemy> activeObj;
        public List<RangeWeaponEnemy> inactiveObj;
        private Vector3 m_position;

        public RangeWeaponEnemy SpawnObj(Vector3 position, Transform parent)
        {
            if (inactiveObj.Count == 0)
            {
                RangeWeaponEnemy obj = GameObject.Instantiate(weaponPrefab, parent);
                obj.transform.position = position;
                m_position = position;
                activeObj.Add(obj);
                return obj;
            }
            else
            {
                RangeWeaponEnemy obj = inactiveObj[0];
                obj.transform.position = position;
                obj.gameObject.SetActive(true);
                obj.transform.SetParent(parent);
                activeObj.Add(obj);
                inactiveObj.RemoveAt(0);
                return obj;
            }
        }

        public void ReleasseObj(RangeWeaponEnemy weapon)
        {
            if (activeObj.Contains(weapon))
            {
                weapon.gameObject.SetActive(false);
                activeObj.Remove(weapon);
                inactiveObj.Add(weapon);
            }
        }

        public void Clear()
        {
            while (activeObj.Count > 0)
            {
                RangeWeaponEnemy obj = activeObj[0];
                obj.gameObject.SetActive(false);
                activeObj.RemoveAt(0);
                inactiveObj.Add(obj);
            }
        }
    }

    public class SpawnManager : MonoBehaviour
    {
        private static SpawnManager m_instance;
        [SerializeField] private WeaponPooling m_weaponPooling;
        [SerializeField] private EnemyWeaponPooling m_redEnemyBullet;
        [SerializeField] private EnemyWeaponPooling m_redBossBullet;
        [SerializeField] private PlayerController m_playerPrefab;
        [SerializeField] private Transform m_pointSpawn;
        private PlayerController m_player;
        public PlayerController Player => m_player;

        public static SpawnManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<SpawnManager>();
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
        }

        public void StartGame()
        {
            if (m_player == null)
            {
                m_player = Instantiate(m_playerPrefab);
            }

            m_player.transform.position = m_pointSpawn.position;
        }

        public WeaponController SpawnWeapon(Vector3 position)
        {
            WeaponPooling pooling = new WeaponPooling();
            pooling = m_weaponPooling;
            WeaponController weaponController = pooling.SpawnObj(position, transform);
            return weaponController;
        }

        public void ReleaseWeapon(WeaponController weaponController)
        {
            WeaponPooling pooling = m_weaponPooling;
            pooling.ReleasseObj(weaponController);
        }

        public RangeWeaponEnemy SpawnEnemyBullet(Vector3 position)
        {
            EnemyWeaponPooling pooling = new EnemyWeaponPooling();
            pooling = m_redEnemyBullet;
            RangeWeaponEnemy weaponController = pooling.SpawnObj(position, transform);
            return weaponController;
        }

        public void ReleaseEnemyBulelt(RangeWeaponEnemy weaponController, bool isBoss)
        {
            if (isBoss == false)
            {
                EnemyWeaponPooling pooling = m_redEnemyBullet;
                pooling.ReleasseObj(weaponController);
            }
            else
            {
                EnemyWeaponPooling pooling = m_redBossBullet;
                pooling.ReleasseObj(weaponController);
            }
        }

        public RangeWeaponEnemy SpawnBossBullet(Vector3 position)
        {
            EnemyWeaponPooling pooling = new EnemyWeaponPooling();
            pooling = m_redBossBullet;
            RangeWeaponEnemy weaponController = pooling.SpawnObj(position, transform);
            return weaponController;
        }
    }
}