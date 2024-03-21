using System.Collections.Generic;
using DG.Tweening;
using EasyButtons;
using Lean.Pool;
using Main.Scripts.Audio;
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
        
        [SerializeField] private ParticleSystem _ingredientDropEffect;
        [Header("Flask")]
        [SerializeField] private Transform FlaskSpawnPoint;
        [SerializeField] private Flask _flaskPrefab;
        [Header("Liquid")] 
        [SerializeField] private MeshRenderer _liquidMeshRenderer;
        private Dictionary<IngredientsType, int> _currentFertilizer = new Dictionary<IngredientsType, int>();
        private Flask _currentFlask;

        public void Enable()=>
            _inputHandler.SubscribeOnInputEvent(KeysEventType.CreatePotion,  CreateFlask);

        public void Disable() =>
            _inputHandler.UnsubscribeFromInputEvent(KeysEventType.CreatePotion,  CreateFlask);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseIngredient ingredient))
            {
                ingredient.Collider.enabled = false;

                if (_currentFertilizer.ContainsKey(ingredient.Type))
                    _currentFertilizer[ingredient.Type] += 1;
                else
                    _currentFertilizer.Add(ingredient.Type, 1);

                var defaultScale = ingredient.transform.localScale;
                ingredient.transform.DOScale(Vector3.zero, 0.2f)
                    .OnComplete(() =>
                    {
                        ingredient.gameObject.SetActive(false);
                        ingredient.transform.localScale = defaultScale;
                        LeanPool.Despawn(ingredient);
                    });

                _ingredientDropEffect.Play();
                SoundController.Instance.PlayClip(SoundType.POOF);
            }
        }

        private HashSet<IngredientRatio> CreateFertilizer()
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
        private void CreateFlask()
        {
            if (_currentFlask == null)
            {
                _currentFlask = LeanPool.Spawn(_flaskPrefab);
                _currentFlask.transform.position = FlaskSpawnPoint.position;
                _currentFlask.OnFlaskDestroy += OnFlaskDestroy;
            }
            SoundController.Instance.PlayClip(SoundType.POOF);
            _currentFlask.Setup(CreateFertilizer());
            Clear();
        }

        private void ChangeLiquidColor()
        {
            
        }
        
        
        private void OnFlaskDestroy()
        {
            _currentFlask.OnFlaskDestroy -= OnFlaskDestroy;
            _currentFlask = null;
        }

        [Button]
        private void Clear()=>
            _currentFertilizer = new Dictionary<IngredientsType, int>();
        

        private void OnDrawGizmos()
        {
            if(FlaskSpawnPoint==null) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(FlaskSpawnPoint.position, 0.2f);
        }
    }
}