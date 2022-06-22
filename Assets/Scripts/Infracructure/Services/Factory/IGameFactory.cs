using UnityEngine;

namespace Assets.Scripts.Infracructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        void CreateTile();
        void CreateHud();
        GameObject CreateMainMenu();
        void CreateUIRoot();
        void CreateBgTapButton(GameObject menuWindow);
        void DropTile();
    }
}
