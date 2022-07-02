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

        private MenuWindow _mainMenu;
        private IGameFactory _factory;
        private IGameStateMachine _stateMachine;

        public void Construct(MenuWindow mainMenu, IGameFactory gameFactory)
        {
            _mainMenu = mainMenu;
            _factory = gameFactory;
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();

            Button.onClick.AddListener(OnPlayGame);
        }

        public void OnGameOver()
        {
            _mainMenu.gameObject.SetActive(true);
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(OnRestart);
        }

        private void OnPlayGame()
        {
            _mainMenu.HideMainMenuWindow();
            _mainMenu.RefreshScoreText();
            
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(OnDropTile);
            _factory.CreateTile();
        }

        private void OnDropTile()
        {
            _factory.DropTile();
        }

        private void OnRestart()
        {
            _stateMachine.Enter<LoadGameState>();
        }
    }
}