using Lean.Pool;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public  static class IngredientFactory 
    {
        public static BaseIngredient CreateIngredient(BaseIngredient prefab, IngredientsType type)
        {
            var clone = LeanPool.Spawn(prefab);
            clone.DisableOrbEffect();
            clone.Collider.enabled = false;
            clone.Rigidbody.useGravity = false;
            clone.Setup(type);
            return clone;
        }
    }
}
