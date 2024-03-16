using UnityEngine;

namespace Main.Scripts.Hand
{
    public class HandMovementLimiter : MonoBehaviour
    {
        [SerializeField] private float _horizontalSize;
        [SerializeField] private float _depthSize;

        public bool CheckOnHorizontalMovement(float position)
            => (transform.position.x-_horizontalSize/2<position && position >transform.position.x+_horizontalSize/2);

        public bool CheckOnDepthMovement(float position)
            => (transform.position.z-_depthSize/2<position && position >transform.position.z+_depthSize/2);
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(_horizontalSize, 1, _depthSize));
        }
    }
}
