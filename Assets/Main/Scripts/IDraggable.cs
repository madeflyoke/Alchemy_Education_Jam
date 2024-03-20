using UnityEngine;

namespace Main.Scripts
{
    public interface IDraggable
    {
        public bool IsDropped { get; }
        public IDraggable GrabItem();
        public void Move(Vector3 position);
        public void DropItem();
    }
}
