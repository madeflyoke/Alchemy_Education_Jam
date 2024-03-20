using System;
using Main.Scripts.Audio;
using Main.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Hand
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private ItemHandler _itemHandler; 
        [Inject] private InputHandler _inputHandler;
        private HandMovementLimiter _movementLimiter;
        private Vector3 _currentPos => transform.position;
        private float _speed = 1.5f;
        private bool _isPressed = false;
        public bool enable = false;
        

        public void Setup(HandMovementLimiter movementLimiter)
        {
            _movementLimiter = movementLimiter;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            _inputHandler.SubscribeOnInputEvent(KeysEventType.MousePrimaryDown,PeekItem );
            _inputHandler.SubscribeOnInputEvent(KeysEventType.MousePrimaryUp,DropItem );
        }

        private void OnDisable()
        {
            _inputHandler.UnsubscribeFromInputEvent(KeysEventType.MousePrimaryDown,PeekItem );
            _inputHandler.UnsubscribeFromInputEvent(KeysEventType.MousePrimaryUp,DropItem );
        }

        private void LateUpdate()
        {
            if(!enable) return;
            var nextPos = _inputHandler.GetAxisRaw() * (_speed * Time.deltaTime);
            nextPos.x = _movementLimiter.CheckOnHorizontalMovement(nextPos.x + _currentPos.x) ? nextPos.x : 0;
            nextPos.y = _movementLimiter.CheckOnDepthMovement(nextPos.y + _currentPos.z) ? nextPos.y : 0;
            transform.position += new Vector3(nextPos.x,0,nextPos.y);
        }

        private void PeekItem() => _itemHandler.TryGrabItem();

        private void DropItem()=> _itemHandler.TryDropItem();
        
    }
}
