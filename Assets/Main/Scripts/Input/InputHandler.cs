using System;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Input
{
    public class InputHandler : MonoBehaviour
    {
        [Inject] private InputConfig _inputConfig;
        public event Action<ButtonType> OnMouseButtonDown;
        public event Action<ButtonType> OnMouseButtonUp;
        public event Action<ButtonType> OnKeyboardButtonClick;

        private Vector2 _axis = Vector2.zero;
        private ButtonPair[] _mouseButton;
        private ButtonPair[] _keyboardButtons;
        public const string MouseXAxis = "Mouse X";
        public const string MouseYAxis = "Mouse Y";
        public bool enable;
        public Vector2 GetAxisRaw() => _axis;
        
        private void Awake()
        {
            _axis = Vector2.zero;
            _mouseButton = _inputConfig.MouseButtonPairs.ToArray();
            _keyboardButtons = _inputConfig.KeyboardButtonPairs.ToArray();
        }

        private void Update()
        {
            if(!enable) return;

            HandleAxis();
            HandleButtonInput();
        }

        private void HandleAxis()
        {
            var rawAxis = Vector2.zero;
            rawAxis.x = UnityEngine.Input.GetAxisRaw(MouseXAxis) * _inputConfig.MouseSensitivity;
            rawAxis.y = UnityEngine.Input.GetAxisRaw(MouseYAxis) * _inputConfig.MouseSensitivity;
            _axis = rawAxis ;
        }

        private void  HandleButtonInput()
        {
            if(_mouseButton!=null)
                foreach (var buttonPair in _mouseButton)
                {
                    if(UnityEngine.Input.GetKeyDown(buttonPair.Key))
                        OnMouseButtonDown?.Invoke(buttonPair.Type);
                    
                    if(UnityEngine.Input.GetKeyUp(buttonPair.Key))
                        OnMouseButtonUp?.Invoke(buttonPair.Type);
                }

            if (_keyboardButtons != null)
                foreach (var buttonPair in _keyboardButtons)
                {
                    if(UnityEngine.Input.GetKeyDown(buttonPair.Key))
                        OnKeyboardButtonClick?.Invoke(buttonPair.Type);
                }
            
        }
    }
    
    public enum ButtonType{
        GrabItem,
        PotionGuide,
        CreatePotion
    }
}
