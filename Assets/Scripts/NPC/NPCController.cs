using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private GameObject m_canvas;
        
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
                m_canvas.gameObject.SetActive(true);
                // SceneManager.LoadScene("UIGame", LoadSceneMode.Additive);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                m_canvas.gameObject.SetActive(false);
            }
        }
    }
}
