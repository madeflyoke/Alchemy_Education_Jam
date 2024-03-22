using UnityEngine;
using Zenject;

namespace Main.Scripts.Ingredients
{
    public class IngredientSpawner : MonoBehaviour, IInteractable, ISpawner
    {
        [Inject] private IngredientsSetup _setup;
        
        [SerializeField] private IngredientsType _spawnItemType;
        [SerializeField] private IngredientSpawnerVisual _visual;
        
        private readonly InteractableTypeP _typeP = InteractableTypeP.ItemSpawner;
        public InteractableTypeP Type() => _typeP;
        public GameObject GetObject() => this.gameObject;

        public IMovable SpawnMovableItem()
        {
            var data = _setup.Ingredients.Find(i => i.Type == _spawnItemType);
            var clone = IngredientFactory.CreateIngredient(data);
            clone.TryGetComponent(out IMovable item);
            return item;
        }
    }
}