using EasyButtons;
using Main.Scripts.Audio;
using UnityEngine;

namespace Main.Scripts.Hand
{
   public class HandGrabberSwitcher : MonoBehaviour
   {
      [SerializeField] private GameObject _grabbedHand;
      [SerializeField] private GameObject _releasedHand;

      private void Awake()
      {
         _grabbedHand.SetActive(false);
         _releasedHand.SetActive(true);
      }

      private void Update()
      {
         if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
         {
            Grab(true);
            SoundController.Instance.PlayClip(SoundType.HOLD_CURSOR);
         }
         else if(UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
         {
            Grab(false);
            SoundController.Instance.PlayClip(SoundType.RELEASE_CURSOR);
         }
      }

      [Button]
      public void Grab(bool isGrabbed)
      {
         _grabbedHand.SetActive(isGrabbed);
         _releasedHand.SetActive(!isGrabbed);
      }
   }
}
