using Assets.Scripts.Infracructure.Services;
using Assets.Scripts.Infracructure.Services.Factory;
using Assets.Scripts.Infracructure.States;
using Assets.Scripts.Infracructure.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Logic
{
    public class BgButton : MonoBehaviour
    {
        public Button Button;
        private GameObject _mainMenu;
        private IGameFactory _factory;
        private IGameStateMachine _stateMachine;

        public void Construct(GameObject mainMenu, IGameFactory gameFactory)
        {
            _mainMenu = mainMenu;
            _factory = gameFactory;
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();

            Button.onClick.AddListener(OnPlayGame);
        }

        public void OnGameOver()
        {
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(OnRestart);
        }

        private void OnPlayGame()
        {
            _mainMenu.gameObject.SetActive(false);
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(OnDropTile);
            _factory.CreateTile();
        }

        private void OnDropTile()
        {
            _factory.CreateTile();
        }

        private void OnRestart()
        {
            _stateMachine.Enter<LoadGameState>();
        }
    }
}