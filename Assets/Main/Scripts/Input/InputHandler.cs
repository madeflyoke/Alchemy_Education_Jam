using System;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Input
{
    public class InputHandler : MonoBehaviour
    {
        //[Inject] private InputConfig _inputConfig;
        public event Action<MouseButtonType> OnMouseButtonDown;
        public event Action<MouseButtonType> OnMouseButtonUp;
        [SerializeField]

            private InputConfig _inputConfig_TEST;

        private Vector2 _axis = Vector2.zero;
        private MouseButtonPair[] _mouseButtonPairs;
        public const string MouseXAxis = "Mouse X";
        public const string MouseYAxis = "Mouse Y";
        public bool enable;
        public Vector2 GetAxisRaw() => _axis;
        
        private void Awake()
        {
            _axis = Vector2.zero;
            _mouseButtonPairs = _inputConfig_TEST.ButtonPairs.ToArray();
            enable = true;
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
            rawAxis.x = UnityEngine.Input.GetAxisRaw(MouseXAxis) * _inputConfig_TEST.MouseSensitivity;
            rawAxis.y = UnityEngine.Input.GetAxisRaw(MouseYAxis) * _inputConfig_TEST.MouseSensitivity;
            _axis = rawAxis ;
        }

        private void  HandleButtonInput()
        {
            if(_mouseButtonPairs!=null)
                foreach (var buttonPair in _mouseButtonPairs)
                {
                    if(UnityEngine.Input.GetKeyDown(buttonPair.Key))
                        OnMouseButtonDown?.Invoke(buttonPair.Type);
                    
                    if(UnityEngine.Input.GetKeyUp(buttonPair.Key))
                        OnMouseButtonUp?.Invoke(buttonPair.Type);
                }
        }
    }
    
    public enum MouseButtonType{
        PrimaryKey,
        SecondaryKey
    }
}
