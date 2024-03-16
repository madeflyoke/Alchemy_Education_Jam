using System;
using System.Collections.Generic;
using Main.Scripts.Ingredients;
using UnityEngine;

namespace Main.Scripts.Flowers
{
    [CreateAssetMenu(fileName = "New Recipes Setup", menuName = "ScriptableObjects/Setups/Create Recipes Setup", order = 1)]
    public class FlowerRecipeSetup : ScriptableObject
    {
        [SerializeField] private List<FlowerRecipeData> _recipes = new List<FlowerRecipeData>();

        public HashSet<IngredientsType> GetFlowerRecipe(FlowerType type)
        {
            var recipe = _recipes.Find(f => f.FlowerType = type).Ingredients;
            if (recipe != null)
            {
                var result = new  HashSet<IngredientsType>();
                foreach (var i in recipe)
                    result.Add(i);
                
                return result;
            }

            return null;
        }  
    }

    [Serializable]
    public class FlowerRecipeData
    {
        public FlowerType FlowerType;
        public List<IngredientsType> Ingredients = new List<IngredientsType>();
    }
}
