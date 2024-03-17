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

        public HashSet<IngredientRatio> GetFlowerRecipe(FlowerType type)
        {
            var recipe = _recipes.Find(f => f.FlowerType == type).Ingredients;
            if (recipe != null)
            {
                var result = new  HashSet<IngredientRatio>();
                foreach (var i in recipe)
                    result.Add(i);
                
                return result;
            }

            return null;
        }

        public FlowerType FindFlowerByRecipe(HashSet<IngredientRatio> ingredients)
        {
            var result = FlowerType.NONE;
            foreach (var recipe in _recipes)
            {
                var currentRecipe = new HashSet<IngredientRatio>(recipe.Ingredients);
                if (currentRecipe.SetEquals(ingredients))
                {
                    result = recipe.FlowerType;
                    break;
                }
            }
            return result;
        }
    }

    [Serializable]
    public class FlowerRecipeData
    {
        public FlowerType FlowerType;
        public List<IngredientRatio> Ingredients = new List<IngredientRatio>();
    }

    [Serializable]
    public struct IngredientRatio
    {
        public IngredientsType Ingredient;
        public int Amount;
    }
}
