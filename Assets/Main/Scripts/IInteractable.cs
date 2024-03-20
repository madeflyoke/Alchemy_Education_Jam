using System;
using UnityEngine;

namespace Main.Scripts
{
    public interface IInteractable
    {
        public Type Type()
        {
            return null;
        }

        public GameObject GetObject();
    }
}
