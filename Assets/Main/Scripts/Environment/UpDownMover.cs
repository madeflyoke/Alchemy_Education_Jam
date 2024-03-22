using System;
using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.Environment
{
    public class UpDownMover : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _height;
        private Tween _tween;

        private void Start()
        {
            StartMove();
        }
        
        private void StartMove()
        {
            _tween?.Kill();
            _tween = _target.DOMoveY(transform.position.y + _height, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
        
        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}
