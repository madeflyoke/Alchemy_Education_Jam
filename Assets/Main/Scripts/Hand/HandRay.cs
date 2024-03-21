using UnityEngine;

namespace Main.Scripts.Hand
{
    public class HandRay : MonoBehaviour
    {
        [SerializeField] private Transform _lowerPoint;
        [SerializeField] private LayerMask _includeLayers;
        private float _defaultDistance;

        private void Start()
        {
            _defaultDistance = _lowerPoint.position.y - transform.position.y;
        }

        private void FixedUpdate()
        {
            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit,100f, _includeLayers))
            {
                ShortUntil(hit.point);
            }
        }

        private void ShortUntil(Vector3 targetPos)
        {
            var targetDistance = targetPos.y - transform.position.y;

            var final = targetDistance / _defaultDistance;

            transform.localScale = new Vector3(transform.localScale.x, final, transform.localScale.z);
        }

        private void SetDefault()
        {
            transform.localScale = Vector3.one;
        }

        public void SetActive(bool isActive)
        {
            SetDefault();
            gameObject.SetActive(isActive);
        }
    }
}
