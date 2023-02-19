using System;
using UnityEngine;

namespace Minigame.Games
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HurdlesPlayer : MonoBehaviour
    {
        public event Action OnDie;
        
        private Rigidbody2D _rb;
        private Collider2D _col;

        public float Speed;
        public float JumpHeight;
        public float Gravity;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<Collider2D>();
        }

        public void SetConfig(float s, float jh, float g)
        {
            Speed = s;
            JumpHeight = jh;
            Gravity = g;
        }

        private void Update()
        {
            _rb.MovePosition(transform.position + Vector3.right* Speed*Time.deltaTime);
        }

        public void Jump()
        {
            _rb.AddForce(Vector3.up * JumpHeight * Time.deltaTime, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("HurdleObstacle"))
            {
                OnDie?.Invoke();
            }
        }
    }
}