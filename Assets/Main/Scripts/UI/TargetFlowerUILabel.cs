using System;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI
{
    public class TargetFlowerUILabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void SetFlowerText(string name)
        {
            if (gameObject.activeSelf==false)
            {
                gameObject.SetActive(true);
            }
            _text.text = name;
        }
    }
}
