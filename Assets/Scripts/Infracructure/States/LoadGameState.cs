using Assets.Scripts.Infracructure.Services.Factory;
using Assets.Scripts.Infracructure.UI.Windows;
using Assets.Scripts.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infracructure.States
{
    public class LoadGameState : IState
    {
        
        private const string MainSceneName = "Main";

        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly LoadingCurtain _curtain;

        public LoadGameState(GameStateMachine gameStateMachine, IGameFactory gameFactory, LoadingCurtain curtain)
        {
            _stateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _curtain = curtain;
        }

        public void Enter()
        {
            _curtain.Show();

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(MainSceneName);

            waitNextScene.completed += OnLoaded;
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        public void OnLoaded(AsyncOperation operation)
        {
            _gameFactory.InitializeTower();
            InitUI();

            BackTexture backTexture = Camera.main.GetComponentInChildren<BackTexture>();
            _gameFactory.SuccessDrop += backTexture.ColorGenerate;

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUI()
        {
            _gameFactory.CreateUIRoot();
            MenuWindow mainMenu = _gameFactory.CreateMenu();
            _gameFactory.CreateBgTapButton(mainMenu);
        }
    }
}
