using System;
using System.Collections.Generic;
using Assets.Scripts.Infracructure.CameraLogic;
using Assets.Scripts.Infracructure.Data;
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
        private const float maxDeltaToSuccessDrop = 0.05f;

        private List<GameObject> _allTiles;
        private GameObject LastTile => _allTiles[_allTiles.Count - 1];

        private Transform _uiRoot;

        private readonly IAssets _assetProvider;
        private readonly IProgressService _progress;

        event Action GameOver;

        public GameFactory(IAssets assetProvider, IProgressService progress)
        {
            _assetProvider = assetProvider;
            _progress = progress;

            GameOver += CleanUp;
        }


        public void CreateTile()
        {
            GameObject tile = _assetProvider.Instantiate(AssetPath.TilePath);

            if (_allTiles == null)
            {
                _allTiles = new List<GameObject>();
                tile.transform.position = GameObject.FindWithTag(FirstTilePointTag).transform.position;
                tile.GetComponent<Tile>().enabled = false;
            }
            else
            {
                tile.transform.localScale = LastTile.transform.localScale;
                tile.transform.position = LastTile.transform.position.AddY(tile.transform.localScale.y);
            }

            _allTiles.Add(tile);
            CameraFollow(tile);
        }


        public void DropTile()
        {
            if (!LastTile.DropTileOn(_allTiles[_allTiles.Count - 2], maxDeltaToSuccessDrop))
            {
                GameOver?.Invoke();
                return;
            }
            CreateTile();
        }

        public void CreateUIRoot()
        {
            GameObject root = _assetProvider.Instantiate(AssetPath.UiRoot);
            _uiRoot = root.transform;
        }

        public GameObject CreateMainMenu()
        {
            return _assetProvider.Instantiate(AssetPath.MainMenuPath, _uiRoot);
        }

        public void CreateHud()
        {
            GameObject hud = _assetProvider.Instantiate(AssetPath.HudPath, _uiRoot);
            HudWindow window = hud.GetComponent<HudWindow>();
            window.RefreshProgress(_progress.Progress);
        }

        public void CreateBgTapButton(GameObject menuWindow)
        {
            GameObject bgButtonGO = _assetProvider.Instantiate(AssetPath.BgTapButtonPath, _uiRoot);
            BgButton button = bgButtonGO.GetComponent<BgButton>();
            button.Construct(menuWindow, this);
            GameOver += button.OnGameOver;
        }

        private void CleanUp()
        {
            _allTiles = null;
        }

        private void CameraFollow(GameObject target) =>
            Camera.main.GetComponent<CameraFollow>().SetFocusTarget(target);
    }
}
