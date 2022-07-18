using System.Collections;
using System.Collections.Generic;
using SaveData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class BtnManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NewGame()
        {
            SceneManager.LoadScene("Level_0");
        }

        
    }
}
