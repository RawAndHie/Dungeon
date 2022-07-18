using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class OtherSpawnPlayer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            SpawnManager.Instance.StartGame();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}