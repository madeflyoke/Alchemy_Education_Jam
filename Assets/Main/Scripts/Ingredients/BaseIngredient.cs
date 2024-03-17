using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public class BaseIngredient : MonoBehaviour, IDragable
    {
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        public IngredientsType Type { get; private set; }
        public IngredientsType Type_TEST;
        public bool IsDropped { get; private set; }
        
        public Rigidbody Rigidbody => _rigidbody;
        public Collider Collider => _collider;

        public IDragable GrabItem()
        {
            if (IsDropped) return null;
            var clone = Instantiate(this);
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

        public void DropItem()
        {
            IsDropped = true;
            _rigidbody.useGravity = true;
            _collider.enabled = true;
        }
    }
}
