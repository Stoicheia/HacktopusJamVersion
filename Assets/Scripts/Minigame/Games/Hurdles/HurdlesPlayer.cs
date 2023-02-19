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

        public float GroundY;

        private Vector2 _velocity;
        private bool _isGrounded;
        private bool _jumpedThisFrame;

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
            _rb.gravityScale = g;
        }

        private void FixedUpdate()
        {
            _velocity.x = Speed;
            _velocity.y -= Gravity;
            if (_velocity.y < 0 && transform.position.y <= GroundY && !_jumpedThisFrame)
            {
                _velocity.y = 0;
                _isGrounded = true;
            }

            if (_jumpedThisFrame) _jumpedThisFrame = false;
            _rb.MovePosition((Vector2)transform.position + _velocity*Time.deltaTime);
        }

        public void Jump()
        {
            if (!_isGrounded) return;
            _velocity.y = JumpHeight;
            _jumpedThisFrame = true;
            _isGrounded = false;
        }

        public void Die()
        {
            OnDie?.Invoke();
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, _col.bounds.extents.y + 1f);
        }
    }
}