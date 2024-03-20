using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
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
        [SerializeField] private CanvasGroup backGroup;
        private HashSet<IngredientRatio> _currentRecipe;
        private FlowerType _currentFlower;
        private StringBuilder _recipeDesc;
        private bool _isActive;

        private void Start()
        {
            _inputHandler.SubscribeOnInputEvent(KeysEventType.ShowPotionGuide, Show);
            _inputHandler.SubscribeOnInputEvent(KeysEventType.ShowPotionGuide, Hide);
            _recepiesWindow.gameObject.SetActive(false);
            _isActive = false;
        }

        private void OnDisable()
        {
            _inputHandler.UnsubscribeFromInputEvent(KeysEventType.ShowPotionGuide, Show);
            _inputHandler.UnsubscribeFromInputEvent(KeysEventType.ShowPotionGuide, Hide);
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

        private void Show()
        {
            if (_isActive) return;

            _recepiesWindow.gameObject.SetActive(true);
            backGroup.DOFade(1, 0.2f).OnComplete(() => { _isActive = true; });
        }

        private void Hide()
        {
            if (!_isActive) return;

            backGroup.DOFade(0, 0.2f).OnComplete(() => { _isActive = false; });
            _recepiesWindow.gameObject.SetActive(false);
        }
    }
}