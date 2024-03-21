using DG.Tweening;
using Lean.Pool;
using Main.Scripts.Ingredients;
using UnityEngine;

namespace Main.Scripts.Craft
{
    public class BoilerSlot : MonoBehaviour
    {
        [SerializeField] private float _scale;
        [SerializeField] private float swimHeight;

        private BaseIngredient _ingredient;
        public IngredientsType Type { get; private set; }
        private Vector3 _defaultIngredientScale;
        public bool IsEmpty { get; private set; }
        public int Count { get; private set; }

        public void IncreaseCount() => Count++;

        public void Set(BaseIngredient ingredient)
        {
            SetupIngredient(ingredient);
            IsEmpty = false;
            Count++;
            _ingredient.gameObject.SetActive(true);
            PlayAnimation();
        }

        private void SetupIngredient(BaseIngredient ingredient)
        {
            _ingredient = ingredient;
            _ingredient.Collider.enabled = false;
            _ingredient.Rigidbody.useGravity = false;
            _defaultIngredientScale = _ingredient.transform.localScale;
            _ingredient.transform.localScale = _defaultIngredientScale * _scale;
        }

        private void PlayAnimation() =>
            transform.DORotate(new Vector3(0f, 360f, 0f), 5f, RotateMode.FastBeyond360).SetLoops(-1);


        public void Reset()
        {
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