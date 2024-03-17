using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

namespace Main.Scripts.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField, Range(0f,1f)] private float _chanceToPlayResultAnimation;
        
        [Button]
        public void TryPlayResultAnimation(bool isWin)
        {
            if (Random.Range(0f,1f)<=_chanceToPlayResultAnimation)
            {
                if (isWin)
                {
                    _animator.PlayHappyAnimation();
                }
                else
                {
                    _animator.PlayAngryAnimation();
                }
            }
        }
        
    }
}
