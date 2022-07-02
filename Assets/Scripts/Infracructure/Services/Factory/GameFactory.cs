using System;
using System.Collections.Generic;
using Assets.Scripts.Infracructure.CameraLogic;
using Assets.Scripts.Infracructure.Logic;
using Assets.Scripts.Infracructure.Services.AssetManagement;
using Assets.Scripts.Infracructure.Services.PersistentProgress;
using Assets.Scripts.Infracructure.UI.Windows;
using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Infracructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string FirstTilePointTag = "FirstTilePoint";
        private const float maxDeltaToSuccessDrop = 0.08f;

        private List<GameObject> _tower;
        private GameObject ActiveTile => _tower[_tower.Count - 1];
        private GameObject LastTile => _tower[_tower.Count - 2];

        private Transform _uiRoot;

        private readonly IAssets _assetProvider;
        private readonly IProgressService _progress;

        public event Action FailDrop;
        public event Action SuccessDrop;

        public GameFactory(IAssets assetProvider, IProgressService progress)
        {
            _assetProvider = assetProvider;
            _progress = progress;
        }

        public void InitializeTower()
        {
            _tower = new List<GameObject>();

            GameObject tile = _assetProvider.Instantiate(AssetPath.TilePath);
            tile.transform.position = GameObject.FindWithTag(FirstTilePointTag).transform.position;
            tile.GetComponent<Tile>().enabled = false;

            _tower.Add(tile);
        }

        public void CreateTile()
        {
            GameObject tile = _assetProvider.Instantiate(AssetPath.TilePath);

            ApplyPreviousTileTransform(tile);

            _tower.Add(tile);
            CameraFollow(tile);
        }

        public void DropTile()
        {
            if (ActiveTile.TryDropTileOn(LastTile, maxDeltaToSuccessDrop))
            {
                CreateTile();
                _progress.AddScore();
                SuccessDrop?.Invoke();
                return;
            }
            FailDrop?.Invoke();
        }

        public void CreateUIRoot()
        {
            GameObject root = _assetProvider.Instantiate(AssetPath.UiRoot);
            _uiRoot = root.transform;
        }

        public MenuWindow CreateMenu()
        {
            GameObject window = _assetProvider.Instantiate(AssetPath.MenuPath, _uiRoot);

            MenuWindow menu = window.GetComponent<MenuWindow>();
            menu.Construct(_progress);
            menu.RefreshGemsText();

            SuccessDrop += menu.RefreshScoreText;

            return menu;
        }

        public void CreateBgTapButton(MenuWindow menuWindow)
        {
            GameObject bgButtonGO = _assetProvider.Instantiate(AssetPath.BgTapButtonPath, _uiRoot);
            BgButton button = bgButtonGO.GetComponent<BgButton>();
            button.Construct(menuWindow, this);
            FailDrop += button.OnGameOver;
        }

        public void CleanUp()
        {
            FailDrop = null;
            SuccessDrop = null;
            _tower = null;
        }

        private void CameraFollow(GameObject target) =>
            Camera.main.GetComponent<CameraFollow>().SetFocusTarget(target);

        private void ApplyPreviousTileTransform(GameObject tile)
        {
            tile.transform.localScale = ActiveTile.transform.localScale;
            tile.transform.position = ActiveTile.transform.position.AddY(tile.transform.localScale.y);
        }
    }
}
