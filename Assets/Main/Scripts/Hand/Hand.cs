using System;
using Main.Scripts.Audio;
using Main.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Hand
{
    public class Hand : MonoBehaviour
    {
        [Inject] private InputHandler _inputHandler;
        [SerializeField] private ItemHandler _itemHandler;
        private HandMovementLimiter _movementLimiter;
        private Vector3 _currentPos => transform.position;
        private float _speed = 1.5f;
        private bool _isPressed = false;
        private bool _isHoldingItem;
        public bool enable = false;

        public void Setup(HandMovementLimiter movementLimiter)
        {
            _movementLimiter = movementLimiter;
            _inputHandler.OnMouseButtonDown += HandleButtonDownEvent;
            _inputHandler.OnMouseButtonUp += HandleButtonUpEvent;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if(!enable) return;
            
            var nextPos = _inputHandler.GetAxisRaw() * (_speed * Time.deltaTime);
            nextPos.x = _movementLimiter.CheckOnHorizontalMovement(nextPos.x + _currentPos.x) ? nextPos.x : 0;
            nextPos.y = _movementLimiter.CheckOnDepthMovement(nextPos.y + _currentPos.z) ? nextPos.y : 0;
            transform.position += new Vector3(nextPos.x,0,nextPos.y);
        }

        private void HandleButtonDownEvent(ButtonType type)
        {
            switch (type)
            {
                case ButtonType.GrabItem:
                {
                    SoundController.Instance?.PlayClip(SoundType.HOLD_CURSOR);
                    _isPressed = true;
                    _isHoldingItem = _itemHandler.TryGrabItem();
                    break;
                }
            }
        }
        
        private void HandleButtonUpEvent(ButtonType type)
        {
            switch (type)
            {
                case ButtonType.GrabItem:
                {
                    SoundController.Instance?.PlayClip(SoundType.RELEASE_CURSOR);
                    _isPressed = false;
                    _itemHandler.TryDropItem();
                    break;
                }
            }
        }
    }
}
