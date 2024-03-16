using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using Main.Scripts.Ingredients;
using UnityEngine;

namespace Main.Scripts.Boiler
{
    public class Boiler : MonoBehaviour
    {
        private List<IngredientsType> _currentIngredients = new List<IngredientsType>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseIngredient ingredient))
            {
                _currentIngredients.Add(ingredient.Type);
                ingredient.transform.DOScale(Vector3.zero, 0.2f)
                    .OnComplete(() => GameObject.Destroy(ingredient.gameObject));

                //TEST
                var str = new StringBuilder();
                foreach (var item in _currentIngredients)
                {
                    str.Append(item + " ");
                }
                
                Debug.Log(str);
            }
        }

        public List<IngredientsType> CreateFertilizer()
        {
            return _currentIngredients;
        }
    }
}
