using Main.Scripts.Level;
using Main.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Main.Scripts
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private int _maxFps;
        [Inject] private GameplayGuide _gameplayGuide;
        [SerializeField] private LevelController _levelController;
        private void Start()
        {
            Application.targetFrameRate = _maxFps;
            LaunchInitialGuide();
        }

        private void LaunchInitialGuide()
        {
            _gameplayGuide.Init();
            _gameplayGuide.OnGuideFinish += LaunchLevel;
            _gameplayGuide.Show();
        }

        private void LaunchLevel()
        {
           _gameplayGuide.OnGuideFinish -= LaunchLevel;
            _levelController.SetupLevel();
            _levelController.Launch();
        }
    }
}
