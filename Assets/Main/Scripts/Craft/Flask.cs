using System;
using System.Collections.Generic;
using Main.Scripts.Flowers;
using UnityEngine;

namespace Main.Scripts.Craft
{
    public class Flask : MonoBehaviour, IDragable
    {
        public event Action OnFlaskDestroy;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _minHeight;
        private HashSet<IngredientRatio> _fertilizer = new HashSet<IngredientRatio>();
        private Vector3 _defaultPosition;
        public HashSet<IngredientRatio> GetFertilizer() => _fertilizer;

        public void Setup(HashSet<IngredientRatio> fertilizer)
        {
            _fertilizer = fertilizer;
            _defaultPosition = transform.position;

        }

        public IDragable GrabItem()
        {
            if (this == null) return null;
            _rigidbody.useGravity = false;
            _collider.enabled = false;
            return this;
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }

        private void FixedUpdate()
        {
            if (transform.position.y < _minHeight)
                transform.position = _defaultPosition;
        }

        public void DropItem()
        {
            _collider.enabled = true;
            _rigidbody.useGravity = true;
        }

        private void OnDestroy()
        {
            OnFlaskDestroy?.Invoke();
        }
    }
}
