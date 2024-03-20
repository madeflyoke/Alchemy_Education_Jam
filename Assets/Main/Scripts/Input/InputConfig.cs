using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Input
{
    [CreateAssetMenu(fileName = "New InputConfig", menuName = "ScriptableObjects/Configs/Create Input Handler Config", order = 1)]
    public class InputConfig : ScriptableObject
    {
        public event Action OnSettingChange;
        public List<ButtonPair> MouseButtonPairs = new List<ButtonPair>();
        public List<ButtonPair> KeyboardButtonPairs = new List<ButtonPair>();
        public float MouseSensitivity;
        
        public void SaveChanges(){
            OnSettingChange?.Invoke();
        }
    }

    [Serializable]
    public struct ButtonPair
    {
        public InputInteractionType InteractionType;
        public KeysEventType EventType;
        public KeyCode KeyType;
    }

    public enum InputInteractionType
    {
        UP,
        DOWN
    }
}
