using System;
using Lean.Pool;
using Main.Scripts.Hand;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public class BaseIngredient : MonoBehaviour, IDraggable
    {
        public bool IsDropped { get; private set; }
        public IngredientsType Type;
        public Rigidbody Rigidbody => _rigidbody;
        public Collider Collider => _collider;
        public Color Color => _ingredientVisual.RelatedColor;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private IngredientVisual _ingredientVisual;

        public void DisableOrbEffectCostyl() => _ingredientVisual.DisableOrbEffect();
        
        public IDraggable GrabItem()
        {
            if (IsDropped) return null;
            var clone = LeanPool.Spawn(this);
            clone.DisableOrbEffectCostyl();
            clone.Collider.enabled = false;
            clone.Rigidbody.useGravity = false;
            clone.Type = Type;
            return clone;
        }
        
        public void Move(Vector3 position) => transform.position = position;
        
        
        public void DropItem()
        {
            IsDropped = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.useGravity = true;
            _collider.enabled = true;
        }
    }
}
