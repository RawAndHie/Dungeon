using System;
using UnityEngine;

// ReSharper disable All
#pragma warning disable CS0169

namespace SaveData
{
    public class PlayerModel
    {
        public int t;
        public float k;
        public int n;
        public int levelDamage;
        public int levelAmor;
        public int levelHealth;
        public int growthHp;
        public int baseHp;
        public int baseDamage;
        public int growthDamage;
        public float costHp;
        public float costDamage;
        public float damage;
        public float health;
        public float haveCoin;
        public int level;
        public int haveExp;
        public int itemHealth;
        public float attackSpeed;

        //set
        public void EarnCoin(int coin)
        {
            haveCoin += coin;
        }

        public void EarnExp(int exp)
        {
            haveExp += exp;
        }

        public void UpgradeDamage()
        {
            var m = baseDamage + baseDamage * (levelDamage - 1) +
                    (1 / (n * (float) Math.Pow(levelDamage, t))) * (float) Math.Pow((levelDamage - 1), 3)
                    + (float) Math.Pow((levelDamage - 1), k);
            costDamage = m / 6;
            // đưa vào giá trị perDmg, currentDmg, levelDmg, + công thức
            if (haveCoin >= costDamage)
            {
                damage = m;
                levelDamage++;
                haveCoin -= costDamage;
            }
        }

        public void UpradeHp()
        {
            // m/6 = cost
            var m = baseHp + growthHp * (levelHealth - 1) +
                    (1 / (n * (float) Math.Pow(levelHealth, t))) * (float) Math.Pow((levelHealth - 1), 3)
                    + (float) Math.Pow((levelHealth - 1), k);
            costHp = m / 6;
            // đưa vào giá trị perDmg, currentDmg, levelDmg, + công thức
            if (haveCoin >= costHp)
            {
                health = m;
                levelHealth++;
                haveCoin -= costHp;
            }
        }

        //get
        public PlayerModel GetAll()
        {
            return this;
        }

        public float GetHp()
        {
            return health;
        }

        public float GetDamage()
        {
            return damage;
        }
        public float GetCoin()
        {
            return haveCoin;
        }

        public int GetExp()
        {
            return haveExp;
        }

        public float GetCostHp()
        {
            return costHp;
        }

        public float GetCostDamage()
        {
            return costDamage;
        }
    }

    public static class PlayerData
    {
        private const string PLAYER_DATA = "PLAYER_DATA";
        private static PlayerModel m_playerModel;

        static PlayerData()
        {
            m_playerModel = JsonUtility.FromJson<PlayerModel>(PlayerPrefs.GetString(PLAYER_DATA));
            // lấy dữ liệu từ PLAYER_DATA chuyển qua json để đọc
            // nếu chưa có thì tạo mới dữ liệu mặc định
            if (m_playerModel == null)
            {
                m_playerModel = new PlayerModel
                {
                    costDamage = 0,
                    costHp = 0,
                    levelDamage = 1,
                    levelAmor = 1,
                    levelHealth = 1,
                    growthHp = 35,
                    growthDamage = 5,
                    t = 1,
                    k = 1.6f,
                    n = 130,
                    baseHp = 100,
                    baseDamage = 5,
                    damage = 5,
                    health = 100,
                    haveCoin = 100,
                    level = 1,
                    haveExp = 0,
                    itemHealth = 2,
                    attackSpeed = 0.3f
                };
                SaveData();
            }
        }

        private static void SaveData()
        {
            var data = JsonUtility.ToJson(m_playerModel);
            PlayerPrefs.SetString(PLAYER_DATA, data);
        }

        // set
        public static void EarnCoin(int coin)
        {
            m_playerModel.EarnCoin(coin);
            SaveData();
        }

        public static void UpgradeHp()
        {
            m_playerModel.UpradeHp();
            SaveData();
        }

        public static void UpgradeDamage()
        {
            m_playerModel.UpgradeDamage();
            SaveData();
        }
        public static void EarnExp(int exp)
        {
            m_playerModel.EarnExp(exp);
            SaveData();
        }

        // get
        public static PlayerModel GetAll()
        {
            return m_playerModel.GetAll();
        }

        public static float GetHp()
        {
            return m_playerModel.GetHp();
        }

        public static float GetDamage()
        {
            return m_playerModel.GetDamage();
        }
        public static float GetCoin()
        {
            return m_playerModel.GetCoin();
        }

        public static int GetExp()
        {
            return m_playerModel.GetExp();
        }

        public static float GetCostHp()
        {
            return m_playerModel.GetCostHp();
        }

        public static float GetCostDamage()
        {
            return m_playerModel.GetCostDamage();
        }
    }
}