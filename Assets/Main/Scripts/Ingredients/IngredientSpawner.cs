using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Ingredients
{
    public class IngredientSpawner : MonoBehaviour, IInteractable, ISpawner
    {
        [Inject] private IngredientsSetup _setup;
        [SerializeField] private IngredientsType _spawnItemType;
        private readonly InteractableTypeP _typeP = InteractableTypeP.ItemSpawner;
        public InteractableTypeP Type() => _typeP;
        public GameObject GetObject() => this.gameObject;

        public IMovable SpawnMovableItem()
        {
            var prefab = _setup.Ingredients.Find(i => i.Type == _spawnItemType).Ingredient;
            var clone = IngredientFactory.CreateIngredient(prefab, prefab.Type);
            clone.TryGetComponent(out IMovable item);
            return item;
        }
    }
}