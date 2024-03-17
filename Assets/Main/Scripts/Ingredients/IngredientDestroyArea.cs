using System;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public class IngredientDestroyArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseIngredient ingredient))
            {
                if(ingredient.IsDropped)
                    GameObject.Destroy(ingredient.gameObject);
            }
        }
    }
}
