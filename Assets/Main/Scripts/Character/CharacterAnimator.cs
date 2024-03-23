using EasyButtons;
using Main.Scripts.Audio;
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
            SoundController.Instance?.PlayClip(SoundType.HUH, SoundController.Instance.SoundsVolume *0.3f);
        }

        [Button]
        public void PlayHappyAnimation()
        {
            _animator.CrossFadeInFixedTime(CLAP_STATE, 0.25f);
        }

        public void OnClap()
        {
            SoundController.Instance?.PlayClip(SoundType.CLAP, SoundController.Instance.SoundsVolume *0.3f);
        }
    }
}