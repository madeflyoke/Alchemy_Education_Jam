using Lean.Pool;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public static class IngredientFactory
    {
        public static BaseIngredient CreateIngredient(BaseIngredient prefab, IngredientsType type)
        {
            var clone = LeanPool.Spawn(prefab);
            clone.DisableOrbEffectCostyl();
            clone.Collider.enabled = false;
            clone.Rigidbody.useGravity = false;
            clone.SetupIngredientType(type);
            return clone;
        }
    }
}