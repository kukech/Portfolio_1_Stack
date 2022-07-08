using Assets.Scripts.Infracructure.CameraLogic;
using Assets.Scripts.Infracructure.Logic;
using Assets.Scripts.Infracructure.Services.AssetManagement;
using Assets.Scripts.Infracructure.Services.PersistentProgress;
using Assets.Scripts.Infracructure.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Infracructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private Transform _uiRoot;

        private readonly IAssets _assetProvider;
        private readonly IProgressService _progress;

        public Button BgButton { get; set; }
        public Tower Tower { get; set; }

        public GameFactory(IAssets assetProvider, IProgressService progress)
        {
            _assetProvider = assetProvider;
            _progress = progress;
        }

        public void CreateTower()
        {
            GameObject towerGO = _assetProvider.Instantiate(AssetPath.TowerPath);

            Tower tower = towerGO.GetComponent<Tower>();
            tower.Construct(_assetProvider);

            Tower = tower;
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

            return menu;
        }

        public void CreateBgTapButton()
        {
            GameObject bgButtonGO = _assetProvider.Instantiate(AssetPath.BgTapButtonPath, _uiRoot);
            BgButton = bgButtonGO.GetComponent<Button>();
        }

        public void CleanUp()
        {
            Tower = null;
            BgButton.onClick.RemoveAllListeners();
        }
    }
}
