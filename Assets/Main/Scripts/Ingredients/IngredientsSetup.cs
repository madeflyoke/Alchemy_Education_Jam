using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    [CreateAssetMenu(fileName = "New IngredientsSetup", menuName = "ScriptableObjects/Setups/Create Ingredients Setup", order = 1)]
    public class IngredientsSetup : ScriptableObject
    {
        public List<IngredientData> Ingredients = new List<IngredientData>();
    }

    [Serializable]
    public class IngredientData
    {
        public IngredientsType Type;
        public BaseIngredient Ingredient;
    }
}
