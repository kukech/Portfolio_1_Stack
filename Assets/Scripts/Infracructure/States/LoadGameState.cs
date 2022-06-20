using Assets.Scripts.Infracructure.Services;
using Assets.Scripts.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infracructure.States
{
    public class LoadGameState : IState
    {
        private const string FirstTilePointTag = "FirstTilePoint";
        private const string MainSceneName = "Main";

        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private LoadingCurtain _curtain;

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

            waitNextScene.completed += GameInitialize;
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        public void GameInitialize(AsyncOperation operation)
        {
            InitTile();
            InitUI();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitTile()
        {
            GameObject startTilePoint = GameObject.FindWithTag(FirstTilePointTag);
            GameObject startTile = _gameFactory.CreateTile(startTilePoint.transform.position);
            startTile.GetComponent<Tile>().enabled = false;
        }

        private void InitUI()
        {

        }
    }
}
