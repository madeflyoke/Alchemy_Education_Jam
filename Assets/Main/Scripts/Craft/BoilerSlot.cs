using System;
using DG.Tweening;
using Lean.Pool;
using Main.Scripts.Ingredients;
using UnityEngine;

namespace Main.Scripts.Craft
{
    public class BoilerSlot : MonoBehaviour
    {
        [SerializeField] private float _scale;

        private BaseIngredient _ingredient;
        public IngredientsType Type { get; private set; }
        private Vector3 _defaultIngredientScale;
        public bool IsEmpty { get; private set; }
        public int Count { get; private set; }

        private void Start() => IsEmpty = true;

        public void IncreaseCount() => Count++;

        public void Set(BaseIngredient ingredient)
        {
            SetupIngredient(ingredient);
            IsEmpty = false;
            Count++;
            _ingredient.gameObject.SetActive(true);
            Type = _ingredient.Type;
            PlayAnimation();
        }

        private void SetupIngredient(BaseIngredient ingredient)
        {
            _ingredient = ingredient;
            _ingredient.Collider.enabled = false;
            _ingredient.Rigidbody.useGravity = false;
            _ingredient.Rigidbody.velocity = Vector3.zero;
            _defaultIngredientScale = _ingredient.transform.localScale;
            _ingredient.transform.localScale = _defaultIngredientScale * _scale;
            _ingredient.transform.SetParent(transform);
            _ingredient.transform.position = transform.position;
        }

        private void PlayAnimation() =>
            transform.DORotate(new Vector3(0f, 360f, 0f), 5f, RotateMode.FastBeyond360)
                .SetLoops(-1);


        public void Reset()
        {
            if(IsEmpty) return;
            _ingredient.transform.localScale = _defaultIngredientScale;
            _ingredient.gameObject.SetActive(false);
            LeanPool.Despawn(_ingredient);
            _ingredient = null;
            transform.DOKill(false);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            IsEmpty = true;
            Count = 0;
        }
    }
}