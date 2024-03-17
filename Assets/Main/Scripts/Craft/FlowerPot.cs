using System;
using System.Collections.Generic;
using Main.Scripts.Flowers;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Craft
{
    public class FlowerPot : MonoBehaviour
    {
        public event Action<FlowerType> OnFlowerGrow;
        [Inject] private FlowerRecipeSetup _recipes;
        [Inject] private FlowersSetup _flowers;
        [SerializeField] private Transform _spawnPoint;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Flask flask))
                CheckRecipe(flask.GetFertilizer());
        }

        private void CheckRecipe(HashSet<IngredientRatio> recipe)
        {
            var flower = _recipes.FindFlowerByRecipe(recipe);

            if (flower != FlowerType.NONE)
                CreateFlower(flower);
            else
                PlayFailAnimation();
        }

        private void CreateFlower(FlowerType flowerType)
        {
            var newFlower = Instantiate(_flowers.Flowers.Find(f => f.Type == flowerType).Prefab);
            newFlower.transform.position = _spawnPoint.position;
            OnFlowerGrow?.Invoke(flowerType);
        }

        private void  PlayFailAnimation()
        {
            Debug.Log("FAIL");
            OnFlowerGrow?.Invoke(FlowerType.NONE);
        }


        public void ChangePot()
        {
            
        }
    }
}
