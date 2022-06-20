using System.Collections.Generic;
using Assets.Scripts.Infracructure.CameraLogic;
using Assets.Scripts.Infracructure.Data;
using Assets.Scripts.Infracructure.Services.AssetManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Infracructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private List<GameObject> _allTiles;

        private Transform _uiRoot;
        private Button _bgButton;
        private GameObject _mainMenu;

        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void CreateTile(GameObject at)
        {
            GameObject tile = _assetProvider.Instantiate(AssetPath.TilePath, at.transform.position);

            if (_allTiles == null)
            {
                _allTiles = new List<GameObject>();

                tile.GetComponent<Tile>().enabled = false;
            }
            else
            {
                tile.transform.position = tile.transform.position.AddY(tile.transform.localScale.y);
            }

            tile.transform.Rotate(0, 45, 0);
            _allTiles.Add(tile);
            CameraFollow(tile);
        }
        public void CreateUIRoot()
        {
            GameObject root = _assetProvider.Instantiate("UI/UIRoot");
            _uiRoot = root.transform;
        }

        public void CreateMainMenu()
        {
            _mainMenu = _assetProvider.Instantiate(AssetPath.MainMenuPath, _uiRoot);
        }

        public void CreateHud()
        {
            Object.Instantiate(Resources.Load(AssetPath.HudPath), _uiRoot);
        }

        public GameObject CreateBgTapButton()
        {
            GameObject bgButtonGO = _assetProvider.Instantiate(AssetPath.BgTapButtonPath, _uiRoot);
            _bgButton = bgButtonGO.GetComponent<Button>();
            _bgButton.onClick.AddListener(OnPlayGame);
            return bgButtonGO;
        }

        private void OnPlayGame()
        {
            _mainMenu.gameObject.SetActive(false);
            _bgButton.onClick.RemoveAllListeners();
            _bgButton.onClick.AddListener(OnDropTile);
            CreateTile(_allTiles[_allTiles.Count - 1]);
        }

        private void OnDropTile()
        {
            CreateTile(_allTiles[_allTiles.Count - 1]);
        }

        private void CameraFollow(GameObject target) =>
            Camera.main.GetComponent<CameraFollow>().SetFocusTarget(target);

    }
}
