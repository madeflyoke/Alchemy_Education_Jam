using System;
using System.Collections.Generic;
using Lean.Pool;
using Main.Scripts.Flowers;
using Main.Scripts.Ingredients;
using UnityEngine;

namespace Main.Scripts.Craft
{
    public class Flask : MonoBehaviour, IMovable, IInteractable
    {
        public event Action OnFlaskDestroy;
        
        public bool IsDropped { get; private set; }

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _minHeight;
        [SerializeField] private MeshRenderer _liquidMeshRenderer;
        [SerializeField] private InteractableTypeP _type;
        private HashSet<IngredientRatio> _fertilizer = new HashSet<IngredientRatio>();
        private Vector3 _defaultPosition;
        public HashSet<IngredientRatio> GetFertilizer() => _fertilizer;

        public void Setup(HashSet<IngredientRatio> fertilizer, Color color)
        {
            _fertilizer = fertilizer;
            _defaultPosition = transform.position;
            _liquidMeshRenderer.materials[0].color = color;
        }
        
        public InteractableTypeP Type()=> _type;
        public GameObject GetObject()=>this.gameObject;

        public IMovable Peak()
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

        public void Release()
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
