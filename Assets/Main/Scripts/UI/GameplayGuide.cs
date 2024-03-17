using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI
{
    public class GameplayGuide : MonoBehaviour
    {
        public event Action OnGuideFinish;
        [SerializeField] private List<GameObject> _pages = new List<GameObject>();
        [SerializeField] private Button _nextBTN;
        [SerializeField] private Button _backBTN;
        [SerializeField] private Button _closeGuideBTN;
        private int _currentpage = 0;

        public void Init()
        {
            // _nextBTN.onClick.AddListener(NextPage);
            // _backBTN.onClick.AddListener(PrevPage);
            _closeGuideBTN.onClick.AddListener(Hide);
        }

        private void NextPage()
        {
            if (_currentpage < _pages.Count)
            {
                if (_currentpage + 1 == _pages.Count)
                {
                    _nextBTN.gameObject.SetActive(false);
                    _closeGuideBTN.gameObject.SetActive(true);
                }
                else
                {
                    _pages[_currentpage].gameObject.SetActive(false);
                    _currentpage++;
                    _pages[_currentpage].gameObject.SetActive(true);
                }
            }
        }
        
        private void PrevPage()
        {
            if (_currentpage >0)
            {
                _pages[_currentpage].gameObject.SetActive(false);
                _currentpage--;
                _pages[_currentpage].gameObject.SetActive(true);
            }
            _closeGuideBTN.gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _currentpage = 0;
            _pages[_currentpage].gameObject.SetActive(true);
        }

        public void Hide()
        {
            // _nextBTN.onClick.RemoveListener(NextPage);
            // _backBTN.onClick.RemoveListener(PrevPage);
            _closeGuideBTN.onClick.RemoveListener(Hide);
            OnGuideFinish?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
