using System;
using EasyButtons;
using Main.Scripts.Audio;
using Main.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Hand
{
   public class HandGrabberSwitcher : MonoBehaviour
   {
      [Inject] private InputHandler _inputHandler;

      
      [SerializeField] private GameObject _grabbedHand;
      [SerializeField] private GameObject _releasedHand;

      private void OnEnable()
      {
         _inputHandler.SubscribeOnInputEvent(KeysEventType.MousePrimaryDown, SetGrabbed);
         _inputHandler.SubscribeOnInputEvent(KeysEventType.MousePrimaryUp, SetReleased);
         SetReleased();
      }

      private void OnDisable()
      {
         _inputHandler.UnsubscribeFromInputEvent(KeysEventType.MousePrimaryDown, SetGrabbed);
         _inputHandler.UnsubscribeFromInputEvent(KeysEventType.MousePrimaryUp, SetReleased);
      }

      private void SetGrabbed()
      {
         _grabbedHand.SetActive(true);
         _releasedHand.SetActive(false);
      }

      private void SetReleased()
      {
         _grabbedHand.SetActive(false);
         _releasedHand.SetActive(true);
      }
   }
}
