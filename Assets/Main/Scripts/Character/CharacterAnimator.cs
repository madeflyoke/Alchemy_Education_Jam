using EasyButtons;
using UnityEngine;

namespace Main.Scripts.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        private const string CLAP_STATE = "Clap";
        private const string ANGRY_STATE = "Angry";
        
        [SerializeField] private Animator _animator;

        [Button]
        public void PlayAngryAnimation()
        {
            _animator.CrossFadeInFixedTime(ANGRY_STATE, 0.25f);
        }

        [Button]
        public void PlayHappyAnimation()
        {
            _animator.CrossFadeInFixedTime(CLAP_STATE, 0.25f);
        }
    }
}