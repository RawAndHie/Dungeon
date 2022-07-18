using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager m_instance;
        [SerializeField] private AudioSource m_bgMusic;
        [SerializeField] private AudioClip m_bgMusicClip;


        public static AudioManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = FindObjectOfType<AudioManager>();
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

        public void BackgroundMusic()
        {
            if (m_bgMusic == m_bgMusicClip)
            {
                return;
            }

            m_bgMusic.loop = true;
            m_bgMusic.clip = m_bgMusicClip;
            m_bgMusic.Play();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
