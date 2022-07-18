using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All
#pragma warning disable CS0169

namespace SaveData
{
    public class ShopModel
    {
        //x
        public int levelDamage;
        public int levelAmor;
        public int levelHealth;
        public int growthHp;
        public int t ;
        public float k ;
        public int n ;
        
        // get
        //set 
        public void UpgradeHealth(int baseHealth)
        {
            
        }
    }

    public static class ShopData
    {
        private const string SHOP_DATA = "SHOP_DATA";
        private static ShopModel m_shopModel;

        static ShopData()
        {
            m_shopModel = JsonUtility.FromJson<ShopModel>(PlayerPrefs.GetString(SHOP_DATA));
            if (m_shopModel == null)
            {
                m_shopModel = new ShopModel
                {
                    levelDamage = 1,
                    levelAmor = 1,
                    levelHealth = 1,
                    growthHp = 35,
                    t = 1,
                    k = 1.6f ,
                    n = 130
                };
                SaveData();
            }
        }

        private static void SaveData()
        {
            var data = JsonUtility.ToJson(m_shopModel);
            PlayerPrefs.SetString(SHOP_DATA, data);
        }
    }
}
