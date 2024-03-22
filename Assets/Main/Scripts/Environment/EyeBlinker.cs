using System;
using DG.Tweening;
using EasyButtons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Environment
{
    public class EyeBlinker : MonoBehaviour
    {
        [SerializeField] private Transform _eyes;
        [SerializeField] private float _delay;
        private Sequence _seq;


        private void Start()
        {
            StartBlink();
        }

        private void StartBlink()
        {
            _seq?.Kill();
            _seq = DOTween.Sequence();

            _seq
                .SetDelay(_delay)
                .Append(_eyes.DOScaleY(0f, 0.1f).SetLoops(2, LoopType.Yoyo))
                .SetLoops(-1, LoopType.Restart);
        }

        private void OnDisable()
        {
            _seq?.Kill();
        }
    }
}
