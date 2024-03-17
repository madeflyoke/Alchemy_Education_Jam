using Main.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Level
{
    public class GameContext : MonoBehaviour
    {
        [Inject] private GameplayGuide _gameplayGuide;
        [SerializeField] private LevelController _levelController;
        private void Start()
        {
            /*_gameplayGuide.Init();
            _gameplayGuide.OnGuideFinish += LaunchLevel;
            _gameplayGuide.Show();*/
            LaunchLevel();
        }

        private void LaunchLevel()
        {
           // _gameplayGuide.OnGuideFinish -= LaunchLevel;
            _levelController.SetupLevel();
            _levelController.Launch();
        }
    }
}
