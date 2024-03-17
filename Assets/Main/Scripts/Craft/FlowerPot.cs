using System.Collections.Generic;
using Main.Scripts.Flowers;
using UnityEngine;

namespace Main.Scripts.Craft
{
    public class FlowerPot : MonoBehaviour
    {
        [SerializeField] private FlowerRecipeSetup _recipes;
        [SerializeField] private FlowersSetup _flowers;
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
        }

        private void  PlayFailAnimation()
        {
            Debug.Log("FAIL");
        }
    }
}
