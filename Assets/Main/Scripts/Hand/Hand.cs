using System;
using Main.Scripts.Input;
using UnityEngine;

namespace Main.Scripts.Hand
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private ItemHandler _itemHandler;
        [SerializeField] private HandMovementLimiter _movementLimiter;
        private Vector3 _currentPos => transform.position;
        private float _speed = 1f;
        private bool _isPressed = false;
        private bool _isHoldingItem;
        public bool enable = false;
        
        private void Start()
        {
            _inputHandler.OnMouseButtonDown += HandleButtonDownEvent;
            _inputHandler.OnMouseButtonUp += HandleButtonUpEvent;
            enable = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if(!enable) return;
            
            var nextPos = _inputHandler.GetAxisRaw() * (_speed * Time.deltaTime);
            /*nextPos.x = _movementLimiter.CheckOnHorizontalMovement(nextPos.x + _currentPos.x) ? nextPos.x : 0;
            nextPos.y = _movementLimiter.CheckOnDepthMovement(nextPos.y + _currentPos.y) ? nextPos.y : 0;*/
            transform.position += new Vector3(nextPos.x,0,nextPos.y);
        }

        private void HandleButtonDownEvent(MouseButtonType type)
        {
            switch (type)
            {
                case MouseButtonType.PrimaryKey:
                {
                    _isPressed = true;
                    _isHoldingItem = _itemHandler.TryGrabItem();
                    break;
                }
            }
        }
        
        private void HandleButtonUpEvent(MouseButtonType type)
        {
            switch (type)
            {
                case MouseButtonType.PrimaryKey:
                {
                    _isPressed = false;
                    _itemHandler.TryDropItem();
                    break;
                }
            }
        }
    }
}
