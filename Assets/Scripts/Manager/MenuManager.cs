using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// ReSharper disable All

namespace Manager
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private TimelineAsset m_timeLine;
        [SerializeField] private PlayableDirector m_playableDirector;

        private bool isClicked;
        // Start is called before the first frame update
        void Start()
        {
            isClicked = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown && isClicked == false)
            {
                isClicked = true;
                m_playableDirector.Play();
            }
        }
    }

}