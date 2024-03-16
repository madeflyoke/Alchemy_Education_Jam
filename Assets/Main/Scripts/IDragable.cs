using UnityEngine;

namespace Main.Scripts
{
    public interface IDragable
    {
        public IDragable GrabItem();
        public void Move(Vector3 position);
        public void DropItem();
    }
}
