using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class ChestController : MonoBehaviour
    {
        [SerializeField] private Sprite m_imageChest;
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
                GetComponent<SpriteRenderer>().sprite = m_imageChest;
            }
        }
    }
}
