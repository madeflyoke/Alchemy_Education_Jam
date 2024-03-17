using System;
using System.Collections.Generic;
using System.Text;
using Main.Scripts.Flowers;
using Main.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Main.Scripts.UI
{
    public class RecipeHelper : MonoBehaviour
    {
        [Inject] private FlowerRecipeSetup _flowerRecipeSetup;
        [Inject] private InputHandler _inputHandler;
        [SerializeField] private GameObject _recepiesWindow;
        private HashSet<IngredientRatio> _currentRecipe;
        private FlowerType _currentFlower;
        private StringBuilder _recipeDesc;
        private bool _isActive;

        private void Start()
        {
            _inputHandler.OnKeyboardButtonClick += Show;
            _inputHandler.OnKeyboardButtonClick += Hide;
            _recepiesWindow.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _inputHandler.OnKeyboardButtonClick -= Show;
            _inputHandler.OnKeyboardButtonClick -= Hide;
        }


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
        
        public void Show(ButtonType buttonType)
        {
            if (buttonType==ButtonType.PotionGuide)
            {
                _isActive = true;
                _recepiesWindow.gameObject.SetActive(true);
            }
        }
        
        public void Hide(ButtonType buttonType)
        {
            if (buttonType==ButtonType.PotionGuide)
            {
                _isActive = false;
                _recepiesWindow.gameObject.SetActive(false);
            }
        }
    }
}
