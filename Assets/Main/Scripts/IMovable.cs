using UnityEngine;

namespace Main.Scripts
{
    public interface IMovable
    {
        public bool IsDropped { get; set; }
        public IMovable Peak();
        public void Move(Vector3 position);
        public void Release();
    }
}
