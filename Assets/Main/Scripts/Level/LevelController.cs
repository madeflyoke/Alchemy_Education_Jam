using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Main.Scripts.Craft;
using Main.Scripts.Flowers;
using Main.Scripts.Input;
using Main.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Main.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Transform _handStartPoint;
        [SerializeField] private Hand.Hand _handPointer;
        [SerializeField] private Hand.HandMovementLimiter _levelBoarders;
        [SerializeField] private FlowerPot _flowerPot;
        [SerializeField] private Boiler _boiler;
        [SerializeField] private Character.Character _witch;
        [SerializeField] private TargetFlowerUILabel _targetLabel;
        private FlowersSetup _flowersSetup;
        private RecipeHelper _recipeHelper;
        private InputHandler _inputHandler;
        private FlowerType _currentFlower;

        [Inject]
        public void Initialize(InputHandler inputHandler, RecipeHelper recipeHelper, FlowersSetup flowersSetup)
        {
            _flowersSetup = flowersSetup;
            _recipeHelper = recipeHelper;
            _inputHandler = inputHandler;
        }

        public void SetupLevel()
        {
            _inputHandler.enable = true;
            PeakRandomFlower();
            _flowerPot.OnFlowerGrow += CheckResult;
            _handPointer.Setup(_levelBoarders);
        }

        public async void Launch()
        {
            _handPointer.enable = true;
            await UniTask.Delay(100);
            _handPointer.transform.position = _handStartPoint.position;
            _handPointer.gameObject.SetActive(true);
            _boiler.Enable();
        }

        private async void CheckResult(FlowerType type)
        {
            if (type == _currentFlower)
            {
                _boiler.Disable();
                _witch.TryPlayResultAnimation(true);
                await UniTask.Delay(3000);
                PeakRandomFlower();
                _boiler.Enable();
                _flowerPot.ResetPot(true);
            }
            else
            {
                _boiler.Disable();
                _witch.TryPlayResultAnimation(false);
                await UniTask.Delay(3000);
                _boiler.Enable();
                _flowerPot.ResetPot(false);
            }
        }

        private void PeakRandomFlower()
        {
            _currentFlower = _flowersSetup.GetRandomFlower().Type;
            _recipeHelper.SetHelperByFlowerType(_currentFlower);
            var builder = new StringBuilder();
            builder.Append(_currentFlower.ToString());
            builder.Replace("_", " ");
            _targetLabel.SetFlowerText(builder.ToString());
        }
    }
}
