using System;
using UnityEngine;

namespace Main.Scripts.Hand
{
    public class ItemHandler : MonoBehaviour
    {
        [SerializeField] private Transform _handlePoint;
        [SerializeField] private Collider _triggerZone;
        [SerializeField] private HandRay _ray;
        
        private IDragable _itemInZone;
        private IDragable _item;
        private bool _isHandleItem;
        
        public bool TryGrabItem()
        {
            if (_itemInZone != null && _isHandleItem == false)
            {
                if (_itemInZone.GrabItem() != null && !_isHandleItem)
                {
                    _triggerZone.enabled = false;
                    _item = _itemInZone.GrabItem();
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
            if (other.TryGetComponent<IDragable>(out IDragable item))
            {
                _ray.ShortUntil(other.transform.position);
                if(item != null && item!=_itemInZone)
                    _itemInZone = item;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDragable>(out IDragable item))
            {
                _ray.SetDefault();
                if(_itemInZone!=null && item== _itemInZone)
                    _itemInZone = null;
            }
        }
    }
}
