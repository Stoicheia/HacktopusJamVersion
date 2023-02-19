using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HurdlesPlayer : MonoBehaviour
    {
        public event Action OnDie;

        public float GameScale;
        
        private Rigidbody2D _rb;
        private Collider2D _col;
        private SkewedImage _img;
        public List<Sprite> sprites = new List<Sprite>();
        public float spriteFramerate = 0.3f;
        public float frameCounter = 0.3f;
        public int spriteCounter = 0;

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
            _img = GetComponent<SkewedImage>();
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
            if (_velocity.y < 0 && transform.localPosition.y <= GroundY && !_jumpedThisFrame)
            {
                _velocity.y = 0;
                _isGrounded = true;
            }

            if (_jumpedThisFrame) _jumpedThisFrame = false;
            Vector3 newPosition = _rb.position + (Vector2)transform.TransformDirection (GameScale*_velocity*Time.deltaTime);
            _rb.MovePosition (newPosition);

            if(frameCounter > 0)
            {
                frameCounter -= Time.deltaTime;
            }
            
            if(frameCounter <= 0)
            {
                if(spriteCounter != 0)
                {
                    spriteCounter = 0;
                }
                else
                {
                    spriteCounter = 1;
                }
                _img.sprite = sprites[spriteCounter];
                frameCounter = spriteFramerate;
            }
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