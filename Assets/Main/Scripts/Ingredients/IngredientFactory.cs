using Lean.Pool;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public static class IngredientFactory
    {
        public static BaseIngredient CreateIngredient(IngredientData data)
        {
            var clone = LeanPool.Spawn(data.Ingredient);
            clone.Initialize(data.Type, data.RelatedColor);
            return clone;
        }
    }
}