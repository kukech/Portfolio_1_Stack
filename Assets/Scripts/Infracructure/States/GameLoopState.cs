using System;
using Assets.Scripts.Infracructure.CameraLogic;
using Assets.Scripts.Infracructure.Services.Factory;
using Assets.Scripts.Infracructure.Services.PersistentProgress;
using Assets.Scripts.Infracructure.UI.Windows;
using UnityEngine;

namespace Assets.Scripts.Infracructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progress;

        private CameraFollow _camera;
        private MenuWindow _menu;

        public event Action SuccessDrop;


        public GameLoopState(IGameFactory gameFactory, IProgressService progress)
        {
            _gameFactory = gameFactory;
            _progress = progress;
        }

        public void Enter()
        {
            _camera = Camera.main.GetComponent<CameraFollow>();

            _menu = _gameFactory.CreateMenu();

            SuccessDrop += _progress.Progress.AddScore;
            SuccessDrop += _menu.RefreshScoreText;
            SuccessDrop += _camera.GetComponentInChildren<BackTexture>().ColorGenerate;

            _gameFactory.BgButton.onClick.AddListener(OnPlayGame);
        }

        public void Exit()
        {
            _progress.SaveProgress();
            _gameFactory.CleanUp();
        }

        private void OnPlayGame()
        {
            _menu.HideMainMenuWindow();
            _menu.RefreshScoreText();

            _gameFactory.BgButton.onClick.RemoveAllListeners();
            _gameFactory.BgButton.onClick.AddListener(OnDropTile);


            _gameFactory.Tower.TryDropTile();
        }

        private void OnDropTile()
        {
            if (_gameFactory.Tower.TryDropTile())
            {
                SuccessDrop?.Invoke();
                _camera.SetFocusPosition(_gameFactory.Tower.ActiveTile.transform.position);
            }
            else
            {
                _gameFactory.BgButton.onClick.RemoveAllListeners();
                _gameFactory.BgButton.onClick.AddListener(OnRestart);
                _progress.Progress.CalculateGemsFromScore();
                _menu.RefreshGemsText();

                _camera.SetFocusPositionWithSize(_gameFactory.Tower.TowerCenterPos, _gameFactory.Tower.TowerHeight * 2);
            }
        }

        private void OnRestart()
        {
            _menu.ShowMainMenuWindow();
            _gameFactory.Tower.ResetTower();
            _gameFactory.BgButton.onClick.RemoveAllListeners();
            _gameFactory.BgButton.onClick.AddListener(OnPlayGame);
            _camera.SetDefaultSize();
        }
    }
}