using System;
using System.Collections.Generic;
using Lean.Pool;
using Main.Scripts.Audio;
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
        [SerializeField] private GameObject _plant;
        [Space]
        [Header("Particles")]
        [SerializeField] private ParticleSystem _successParticles;
        [SerializeField] private ParticleSystem _neutralParticles;
        [SerializeField] private ParticleSystem _failedParticles;
        private Flower _flower;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Flask flask))
            {
                CheckRecipe(flask.GetFertilizer());
                flask.Despawn();
            }
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
            _neutralParticles.Play();
            SoundController.Instance.PlayClip(SoundType.POOF);
            _plant.SetActive(false);
            _flower = Instantiate(_flowers.Flowers.Find(f => f.Type == flowerType).Prefab);
            _flower.transform.position = _spawnPoint.position;
            OnFlowerGrow?.Invoke(flowerType);
        }

        private void  PlayFailAnimation()
        {
            _failedParticles.Play();
            SoundController.Instance.PlayClip(SoundType.POOF);
            OnFlowerGrow?.Invoke(FlowerType.NONE);
        }

        public void ResetPot(bool good)
        {
            if (good)
            {
                _successParticles.Play();
            }
            else
            {
                _failedParticles.Play();
            }
            SoundController.Instance.PlayClip(SoundType.POOF);

            
            _plant.SetActive(true);
            if(_flower!=null)
                GameObject.Destroy(_flower.gameObject);
        }
    }
}
