using System;
using UnityEngine;

namespace Main.Scripts.Hand
{
    public class ItemHandler : MonoBehaviour
    {
        [SerializeField] private Transform _handlePoint;
        [SerializeField] private Collider _triggerZone;
        private IDragable _itemInZone;
        private IDragable _item;
        private bool _isHandleItem;
        public bool TryGrabItem()
        {
            Debug.Log("TRY_GRAB");
            if (_itemInZone != null && _isHandleItem==false)
            {
                Debug.Log("GRAB");
                _triggerZone.enabled = false;
                _item = _itemInZone.GrabItem();
                _itemInZone = null;
                _isHandleItem = true;
                return true;
            }

            return false;
        }

        public void TryDropItem()
        {
            Debug.Log("TRY_DROP");
            if (_item != null && _isHandleItem)
            {
                Debug.Log("DROP");
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
                if(item != null && item!=_itemInZone)
                    _itemInZone = item;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDragable>(out IDragable item))
            {
                if(_itemInZone!=null && item== _itemInZone)
                    _itemInZone = null;
            }
        }
    }
}
