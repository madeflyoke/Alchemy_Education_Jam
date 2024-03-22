using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Flowers
{
    [CreateAssetMenu(fileName = "New Flowers Setup", menuName = "ScriptableObjects/Setups/Create Flowers Setup", order = 1)]
    public class FlowersSetup : ScriptableObject
    {
        public List<FlowerData> Flowers = new List<FlowerData>();

        public FlowerData GetRandomFlower(FlowerType prewType = FlowerType.NONE)
        {
            var index = Random.Range(0, Flowers.Count);
            if (prewType == FlowerType.NONE || Flowers[index].Type != prewType) return Flowers[index];

            if (index == Flowers.Count - 1)
                index--;
            else
                index++;
            return Flowers[index];
        }
    }

    [Serializable]
    public class FlowerData
    {
        public FlowerType Type;
        public Flower Prefab;
    }
}
