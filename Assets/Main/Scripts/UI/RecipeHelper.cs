using System.Collections.Generic;
using System.Text;
using Main.Scripts.Flowers;
using UnityEngine;
using Zenject;

namespace Main.Scripts.UI
{
    public class RecipeHelper : MonoBehaviour
    {
        [Inject] private FlowerRecipeSetup _flowerRecipeSetup;
        private HashSet<IngredientRatio> _currentRecipe;
        private FlowerType _currentFlower;
        private StringBuilder _recipeDesc;
        
        public void SetHelperByFlowerType(FlowerType type)
        {
            _currentFlower = type;
            _currentRecipe = _flowerRecipeSetup.GetFlowerRecipe(_currentFlower);
            _recipeDesc = new StringBuilder();
            foreach (var item in _currentRecipe)
            {
                _recipeDesc.Append($"{item.Ingredient} (x{item.Amount})");
            }
        }
        
        public void Show()
        {
            
        }
        
        public void Hide()
        {
            
        }
    }
}
