using System;
using System.Collections.Generic;
using Main.Scripts.Flowers;
using UnityEngine;

namespace Main.Scripts.Craft
{
    public class Flask : MonoBehaviour, IDragable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        private HashSet<IngredientRatio> _fertilizer = new HashSet<IngredientRatio>();
        private bool _grabbed;
        public HashSet<IngredientRatio> GetFertilizer() => _fertilizer;

        public void Setup(HashSet<IngredientRatio> fertilizer)
        {
            _fertilizer = fertilizer;
        }

        public IDragable GrabItem()
        {
            if (_grabbed) return null;

            _grabbed = true;
            _rigidbody.useGravity = false;
            _collider.enabled = false;
            return this;
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }

        public void DropItem()
        {
            _collider.enabled = true;
            _rigidbody.useGravity = true;
        }
    }
}
