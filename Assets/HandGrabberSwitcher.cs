using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

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
      if (Input.GetKeyDown(KeyCode.Mouse0))
      {
         Grab(true);
      }
      else if(Input.GetKeyUp(KeyCode.Mouse0))
      {
         Grab(false);
      }
   }

   [Button]
   public void Grab(bool isGrabbed)
   {
      _grabbedHand.SetActive(isGrabbed);
      _releasedHand.SetActive(!isGrabbed);
   }
}
