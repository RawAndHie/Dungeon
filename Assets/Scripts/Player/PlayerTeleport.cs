using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay;
using Item;
using UnityEngine;

namespace Player
{
    public class PlayerTeleport : MonoBehaviour
    {
        private GameObject m_currentLocation;
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
            if (col.gameObject.CompareTag("LevelLoad"))
            {
                Debug.Log("vacham");
                Debug.Log("eee");
                // this.transform.position = m_currentLocation.GetComponent<Teleport>().GetPotal().position;
            }
        }

    }

}