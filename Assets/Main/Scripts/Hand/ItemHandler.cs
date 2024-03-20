using System;
using UnityEngine;

namespace Main.Scripts.Hand
{
    public class ItemHandler : MonoBehaviour
    {
        [SerializeField] private Transform _handlePoint;
        [SerializeField] private Collider _triggerZone;
        [SerializeField] private HandRay _ray;
        
        private IDraggable _itemInZone;
        private IDraggable _item;
        private bool _isHandleItem;
        
        public bool TryGrabItem()
        {
            if (_itemInZone != null && _isHandleItem == false)
            {
                var newItem = _itemInZone.GrabItem();
                if (newItem != null && !_isHandleItem)
                {
                    _ray.SetActive(false);
                    _triggerZone.enabled = false;
                    _item = newItem;
                    _itemInZone = null;
                    _isHandleItem = true;
                    return true;
                }
            }
            
            return false;
        }

        public void TryDropItem()
        {
            if (_item != null && _isHandleItem)
            {
                _ray.SetActive(true);
                _isHandleItem = false;
                _item.DropItem();
                _item = null;
                _triggerZone.enabled = true;
            }
        }

        private void Update()
        {
            if (_item != null && _isHandleItem)
                _item.Move(_handlePoint.position);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDraggable>(out IDraggable item))
            {
                if (item != null && item != _itemInZone)
                {
                    _itemInZone = item;
                    if (item.IsDropped==false)
                    {
                        _ray.ShortUntil(other.transform.position);
                    }
                }
            }
        }
        

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDraggable>(out IDraggable item))
            {
                if (_itemInZone != null && item == _itemInZone)
                {
                    _ray.SetDefault();
                    _itemInZone = null;
                }
            }
        }
    }
}
