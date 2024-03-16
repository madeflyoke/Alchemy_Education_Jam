using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Input
{
    [CreateAssetMenu(fileName = "New InputConfig", menuName = "ScriptableObjects/Configs/Create Input Handler Config", order = 1)]
    public class InputConfig : ScriptableObject
    {
        public event Action OnSettingChange;
        public List<MouseButtonPair> ButtonPairs = new List<MouseButtonPair>();
        public float MouseSensitivity;
        
        public void SaveChanges(){
            OnSettingChange?.Invoke();
        }
    }

    [Serializable]
    public struct MouseButtonPair
    {
        public MouseButtonType Type;
        public KeyCode Key;
    }
}
