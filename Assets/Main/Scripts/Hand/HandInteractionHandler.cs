using System;
using Main.Scripts.Audio;
using Main.Scripts.Ingredients;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Hand
{
    public class HandInteractionHandler : MonoBehaviour
    {
        [Inject] private SoundController _sound;
        [SerializeField] private Transform _handlePoint;
        [SerializeField] private Collider _triggerZone;
        [SerializeField] private HandRay _ray;
        private IMovable _movableItem;
        private IInteractable _itemInZone;

        public void TryInteract()
        {
            if (_itemInZone == null) return;

            switch (_itemInZone.Type())
            {
                case InteractableTypeP.MovableObject:
                {
                    if (_itemInZone.GetObject() != null)
                        if (_itemInZone.GetObject().TryGetComponent(out IMovable movableItem))
                            PeakItem(movableItem);
                    break;
                }
                case InteractableTypeP.ItemSpawner:
                {
                    if (_itemInZone.GetObject() != null)
                        if (_itemInZone.GetObject().TryGetComponent(out ISpawner spawner))
                            PeakItem(spawner.SpawnMovableItem());
                    break;
                }
            }
        }

        private void PeakItem(IMovable item)
        {
            _ray.SetActive(false);
            _triggerZone.enabled = false;
            _movableItem = item.Peak();
            _itemInZone = null;
            _sound.PlayClip(SoundType.HOLD_CURSOR);
        }

        public void Release()
        {
            if (_movableItem != null)
            {
                _sound.PlayClip(SoundType.RELEASE_CURSOR);
                _ray.SetActive(true);
                _movableItem.Release();
                _movableItem = null;
            }

            _triggerZone.enabled = true;
        }

        private void Update()
        {
            if (_movableItem != null)
                _movableItem.Move(_handlePoint.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out IInteractable item)) return;
            if (item != null && item != _itemInZone)
                _itemInZone = item;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IInteractable>(out IInteractable item)) return;
            if (item == _itemInZone)
                _itemInZone = null;
        }
    }
}