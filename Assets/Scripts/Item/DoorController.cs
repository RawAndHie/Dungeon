using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item 
{
    public class DoorController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                this.gameObject.SetActive(false);
            }
        }

    }

}