using System;
using System.Collections.Generic;
using Lean.Pool;
using Main.Scripts.Flowers;
using UnityEngine;

namespace Main.Scripts.Craft
{
    public class Flask : MonoBehaviour, IDraggable, IInteractable
    {
        public event Action OnFlaskDestroy;
        
        public bool IsDropped { get; private set; }

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _minHeight;
        [SerializeField] private MeshRenderer _water;
        private HashSet<IngredientRatio> _fertilizer = new HashSet<IngredientRatio>();
        private Vector3 _defaultPosition;
        public HashSet<IngredientRatio> GetFertilizer() => _fertilizer;

        public void Setup(HashSet<IngredientRatio> fertilizer, Color color)
        {
            _fertilizer = fertilizer;
            _defaultPosition = transform.position;
            _water.materials[0].color = color;
        }

        public Type Type()
        {
            return typeof(IDraggable);
        }

        public GameObject GetObject()
        {
            return this.gameObject;
        }

        public IDraggable GrabItem()
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
            IsDropped = true;
            _collider.enabled = true;
            _rigidbody.useGravity = true;
        }

        public void Despawn()
        {
            LeanPool.Despawn(this);
            OnFlaskDestroy?.Invoke();
        }

       
    }
}
