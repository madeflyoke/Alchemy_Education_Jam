using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using EasyButtons;
using Main.Scripts.Flowers;
using Main.Scripts.Ingredients;
using Main.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Craft
{
    public class Boiler : MonoBehaviour
    {
        [Inject] private InputHandler _inputHandler;
        [SerializeField] private Transform FlaskSpawnPoint;
        [SerializeField] private Flask _prefab;
        private Dictionary<IngredientsType, int> _currentFertilizer = new Dictionary<IngredientsType, int>();
        private Flask _currentFlask;

        public void Enable()
        {
            _inputHandler.OnKeyboardButtonClick += HandlePotionEvent;
        }

        public void Disable()
        {
            _inputHandler.OnKeyboardButtonClick -= HandlePotionEvent;
        }

        private void HandlePotionEvent(ButtonType type)
        {
            if (type == ButtonType.CreatePotion)
                CreateFlask();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseIngredient ingredient))
            {
                ingredient.Collider.enabled = false;
                Debug.Log(ingredient.Type);
                if (_currentFertilizer.ContainsKey(ingredient.Type))
                    _currentFertilizer[ingredient.Type] += 1;
                else
                    _currentFertilizer.Add(ingredient.Type, 1);

                ingredient.transform.DOScale(Vector3.zero, 0.2f)
                    .OnComplete(() => GameObject.Destroy(ingredient.gameObject));
                //TEST
                var str = new StringBuilder();
                foreach (var item in _currentFertilizer)
                {
                    str.Append(item + " ");
                }

                Debug.Log(str);
            }
        }

        public HashSet<IngredientRatio> CreateFertilizer()
        {
            var recipe = new HashSet<IngredientRatio>();
            foreach (var ingredient in _currentFertilizer)
            {
                recipe.Add(new IngredientRatio()
                {
                    Ingredient = ingredient.Key,
                    Amount = ingredient.Value
                });
            }

            return recipe;
        }

        [Button]
        public void CreateFlask()
        {
            if (_currentFlask == null)
            {
                _currentFlask = Instantiate(_prefab);
                _currentFlask.transform.position = FlaskSpawnPoint.position;
                _currentFlask.OnFlaskDestroy += OnFlaskDestroy;
            }
           
            _currentFlask.Setup(CreateFertilizer());
            Clear();
        }

        private void OnFlaskDestroy()
        {
            _currentFlask.OnFlaskDestroy -= OnFlaskDestroy;
            _currentFlask = null;
        }

        [Button]
        public void Clear()=>
            _currentFertilizer = new Dictionary<IngredientsType, int>();
        

        private void OnDrawGizmos()
        {
            if(FlaskSpawnPoint==null) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(FlaskSpawnPoint.position, 0.2f);
        }
    }
}