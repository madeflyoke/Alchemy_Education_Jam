using System.Collections.Generic;
using DG.Tweening;
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
        [SerializeField] private Transform FlaskSpawnPoint;
        [SerializeField] private Flask _flaskPrefab;
        [SerializeField] private ParticleSystem _ingredientDropEffect;
        [SerializeField] private List<BoilerSlot> _slots = new List<BoilerSlot>();
        [SerializeField] private Color _defaultColor;
        [SerializeField] private MeshRenderer _water;
        private Color _currentColor;
        private Dictionary<IngredientsType, int> _currentFertilizer = new Dictionary<IngredientsType, int>();
        private Flask _currentFlask;

        private void Start()=> _water.materials[0].color = _defaultColor;

        public void Enable() =>
            _inputHandler.SubscribeOnInputEvent(KeysEventType.CreatePotion, CreateFlask);

        public void Disable() =>
            _inputHandler.UnsubscribeFromInputEvent(KeysEventType.CreatePotion,  CreateFlask);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseIngredient ingredient))
            {
                _currentColor = Color.Lerp(_currentColor, ingredient.Color,0.5f);
                _water.materials[0].color = _currentColor;
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
                        bool alreadyInSlot = false;
                        foreach (var slot in _slots)
                            if (slot.Type == ingredient.Type)
                            {
                                slot.IncreaseCount();
                                alreadyInSlot = true;
                                break;
                            }

                        if (alreadyInSlot)
                            LeanPool.Despawn(ingredient);
                        else
                            foreach (var slot in _slots)
                                if (slot.IsEmpty)
                                {
                                    slot.Set(ingredient);
                                    break;
                                }
                                   
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

        private void CreateFlask()
        {
            if (_currentFlask == null)
            {
                _currentFlask = LeanPool.Spawn(_flaskPrefab);
                _currentFlask.transform.position = FlaskSpawnPoint.position;
                _currentFlask.OnFlaskDestroy += OnFlaskDestroy;
            }

            SoundController.Instance.PlayClip(SoundType.POOF);
            _currentFlask.Setup(CreateFertilizer(), _currentColor);
            Clear();
        }

        private void OnFlaskDestroy()
        {
            _currentFlask.OnFlaskDestroy -= OnFlaskDestroy;
            _currentFlask = null;
        }

        private void Clear()
        {
            _currentFertilizer = new Dictionary<IngredientsType, int>();
            _water.materials[0].color = _defaultColor;
            foreach (var slot in _slots)
                slot.Reset();
        }

        private void OnDrawGizmos()
        {
            if (FlaskSpawnPoint == null) return;
            Gizmos.color = new Color(0,1,1,0.5f);
            Gizmos.DrawSphere(FlaskSpawnPoint.position, 0.2f);
        }
    }
}