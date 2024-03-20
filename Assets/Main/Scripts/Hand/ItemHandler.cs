using System;
using Main.Scripts.Audio;
using Main.Scripts.Ingredients;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Hand
{
    public class ItemHandler : MonoBehaviour
    {
        [Inject] private SoundController _sound;
        [SerializeField] private Transform _handlePoint;
        [SerializeField] private Collider _triggerZone;
        [SerializeField] private HandRay _ray;
        private IDraggable _currentItem;
        private IDraggable _itemInZone;

        public void TryGrabItem()
        {
            if (_itemInZone != null)
            {
                var newItem = _itemInZone.GrabItem();
                if (newItem != null)
                {
                    _ray.SetActive(false);
                    _triggerZone.enabled = false;
                    _currentItem = newItem;
                    _itemInZone = null;
                    _sound.PlayClip(SoundType.HOLD_CURSOR);
                }
            }
        }

        public void TryDropItem()
        {
            if (_currentItem != null )
            {
                _sound.PlayClip(SoundType.RELEASE_CURSOR);
                _ray.SetActive(true);
                _currentItem.DropItem();
                _currentItem = null;
            }
            _triggerZone.enabled = true;
        }

        private void Update()
        {
            if (_currentItem != null)
                _currentItem.Move(_handlePoint.position);
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
                if (item == _itemInZone)
                {
                    _ray.SetDefault();
                    _itemInZone = null;
                }
            } 
           
        }
    }
}
