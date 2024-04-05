using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public class BaseIngredient : MonoBehaviour, IMovable
    {
        public bool IsDropped { get; set; }
        public IngredientsType Type { get; private set; }
        public Color Color { get; private set; }
        public Transform VisualPart => _visualPart;

        [SerializeField] private Transform _visualPart;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        public void Initialize(IngredientsType type, Color relatedColor)
        {
            SetPhysicsActive(false);

            if (type != IngredientsType.DEFAULT)
                Type = type;
            
            Color = relatedColor;
            IsDropped = false;
        }

        public IMovable Peak()
        {
            if (IsDropped) return null;
            SetPhysicsActive(false);
            transform.DOKill(false);
            gameObject.SetActive(true);
            return this;
        }

        public void SetPhysicsActive(bool isActive)
        {
            _rigidbody.velocity = Vector3.zero;
            SetColliderActive(isActive);
            _rigidbody.useGravity = isActive;
        }

        public void SetColliderActive(bool isActive)
        {
            _collider.enabled = isActive;
        }
        
        public void Move(Vector3 position) => transform.position = position;

        public void Release()
        {
            IsDropped = true;
            SetPhysicsActive(true);
        }
    }
}