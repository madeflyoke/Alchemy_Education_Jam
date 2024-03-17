using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using EasyButtons;
using Main.Scripts.Flowers;
using Main.Scripts.Ingredients;
using UnityEngine;

namespace Main.Scripts.Craft
{
    public class Boiler : MonoBehaviour
    {
        [SerializeField] private Transform FlaskSpawnPoint;
        [SerializeField] private Flask _prefab;
        private Dictionary<IngredientsType, int> _currentFertilizer = new Dictionary<IngredientsType, int>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseIngredient ingredient))
            {
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
            var newFlask = Instantiate(_prefab);
            newFlask.transform.position = FlaskSpawnPoint.position;
            newFlask.Setup(CreateFertilizer());
            _currentFertilizer = new Dictionary<IngredientsType, int>();
        }

        [Button]
        public void Clear()
        {
            _currentFertilizer = new Dictionary<IngredientsType, int>();
        }

        private void OnDrawGizmos()
        {
            if(FlaskSpawnPoint==null) return;
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(FlaskSpawnPoint.position, 0.2f);
                
        }
    }
}