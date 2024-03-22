using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public class BaseIngredient : MonoBehaviour, IMovable
    {
        public bool IsDropped { get; private set; }
        public IngredientsType Type { get; private set; }
        public Rigidbody Rigidbody => _rigidbody;
        public Collider Collider => _collider;
        public Color Color => _ingredientVisual.RelatedColor;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private IngredientVisual _ingredientVisual;

        public void DisableOrbEffectCostyl() => _ingredientVisual.DisableOrbEffect();

        public void SetupIngredientType(IngredientsType type)
        {
            if (type != IngredientsType.DEFAULT)
                Type = type;
        }

        public IMovable Peak()
        {
            if (IsDropped) return null;
            _collider.enabled = false;
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            return this;
        }

        public void Move(Vector3 position) => transform.position = position;

        public void Release()
        {
            IsDropped = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.useGravity = true;
            _collider.enabled = true;
        }
    }
}