using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed = 1f;

        private Vector3 m_moveDelta;
        private Rigidbody2D m_rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            float verticalMove = Input.GetAxisRaw("Vertical");

            m_moveDelta = new Vector3(horizontalMove, verticalMove, 0);


            // window or editor 
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (m_moveDelta.x > 0)
                {
                    transform.localScale = Vector3.one;
                }
                else if (m_moveDelta.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                // transform.Translate(m_moveDelta * Time.deltaTime * m_moveSpeed);
                m_rigidbody.velocity = new Vector2(horizontalMove, verticalMove) * m_moveSpeed;
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
            }
        }
    }
}