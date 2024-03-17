using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Flowers
{
    [CreateAssetMenu(fileName = "New Flowers Setup", menuName = "ScriptableObjects/Setups/Create Flowers Setup", order = 1)]
    public class FlowersSetup : ScriptableObject
    {
        public List<FlowerData> Flowers = new List<FlowerData>();
    }

    [Serializable]
    public class FlowerData
    {
        public FlowerType Type;
        public Flower Prefab;
    }
}
