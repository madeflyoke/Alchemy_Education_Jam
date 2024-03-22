using System;
using UnityEngine;

namespace Main.Scripts
{
    public interface IInteractable
    {
        public InteractableTypeP Type();

        public GameObject GetObject();
    }

    public enum InteractableTypeP
    {
        MovableObject,
        ItemSpawner
        
    }
}
