using System;
using EasyButtons;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    public class IngredientVisual : MonoBehaviour
    {
        public Color RelatedColor => _relatedColor;
       
        [SerializeField] private ParticleSystem _orbEffect;
        private Color _relatedColor;

        private void Start()
        {
            var color = _orbEffect.main.startColor.color;
            color.a = 1f;

            _relatedColor = color;
        }
    }
}
