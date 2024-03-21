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
        [SerializeField] private Color _color;
        public bool IsDropped { get; private set; }
        public IngredientsType Type;
        public Rigidbody Rigidbody => _rigidbody;
        public Collider Collider => _collider;
        public Color Color => _color;

        public IDraggable GrabItem()
        {
            if (IsDropped) return null;
            var clone = LeanPool.Spawn(this);
            clone.DisableOrbEffect();
            clone.Collider.enabled = false;
            clone.Rigidbody.useGravity = false;
            clone.Type = Type;
            return clone;
        }
        
        public void Move(Vector3 position) => transform.position = position;

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
            _rigidbody.velocity = Vector3.zero;
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
