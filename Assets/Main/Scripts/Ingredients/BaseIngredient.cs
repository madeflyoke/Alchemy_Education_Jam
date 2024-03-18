using System;
using Lean.Pool;
using Main.Scripts.Hand;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public class BaseIngredient : MonoBehaviour, IDraggable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _orbEffect;
        
        public bool IsDropped { get; private set; }

        public IngredientsType Type { get; private set; }
        public IngredientsType Type_TEST;
        
        public Rigidbody Rigidbody => _rigidbody;
        public Collider Collider => _collider;

        public IDraggable GrabItem()
        {
            if (IsDropped) return null;
            var clone = LeanPool.Spawn(this);
            clone.DisableOrbEffect();
            clone.Collider.enabled = false;
            clone.Rigidbody.useGravity = false;
            //clone.Setup(Type);
            clone.Setup(Type_TEST);
            return clone;
        }

        public void Setup(IngredientsType type) => Type = type;
        
        public void Move(Vector3 position)
        {
            transform.position = position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ItemHandler>(out _))
            {
                PauseParticle(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ItemHandler>(out _))
            {
                PauseParticle(true);
            }
        }

        private void PauseParticle(bool isPaused)
        {
            var module = _orbEffect.rotationOverLifetime;

            var value = isPaused ? 1f : 5f;

            module.xMultiplier = value;
            module.yMultiplier = value;
            module.zMultiplier = value;
        }

        public void DropItem()
        {
            IsDropped = true;
            _rigidbody.useGravity = true;
            _collider.enabled = true;
        }

        public void DisableOrbEffect()
        {
            _orbEffect.gameObject.SetActive(false);
            _orbEffect.Stop();
        }

        private void OnValidate()
        {
            _orbEffect ??= GetComponentInChildren<ParticleSystem>();
            if (_orbEffect)
            {
                PauseParticle(true);
            }
        }
    }
}
