using Main.Scripts.Audio;
using Main.Scripts.Flowers;
using Main.Scripts.Input;
using Main.Scripts.Level;
using Main.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Main.Resources
{
    public class MainInstaller : MonoInstaller
    {
        [Header("ScriptableObjects")]
        [SerializeField] private FlowerRecipeSetup _flowerRecipeSetup;
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private FlowersSetup _flowersSetup;
        [Space]
        [Header("Services")]
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private SoundController _soundController;
        [Space]
        [Header("UI")]
        [SerializeField] private GameplayGuide _gameplayGuide;
        [SerializeField] private RecipeHelper _recipeHelper;

        public override void InstallBindings()
        {
            BindConfigs();
            BindServices();
            BindUI();
        }

        private void BindConfigs()
        {
            Container.Bind<FlowerRecipeSetup>().FromInstance(_flowerRecipeSetup).AsSingle().NonLazy();
            Container.Bind<FlowersSetup>().FromInstance(_flowersSetup).AsSingle().NonLazy();
            Container.Bind<InputConfig>().FromInstance(_inputConfig).AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<InputHandler>()
                .FromInstance(_inputHandler)
                .AsSingle()
                .NonLazy();
            Container.Bind<SoundController>()
                .FromInstance(_soundController)
                .AsSingle()
                .NonLazy();
        }

        private void BindUI()
        {
            Container.Bind<GameplayGuide>()
                .FromInstance(_gameplayGuide)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<RecipeHelper>()
                .FromInstance(_recipeHelper)
                .AsSingle()
                .NonLazy();
        }
    }
}