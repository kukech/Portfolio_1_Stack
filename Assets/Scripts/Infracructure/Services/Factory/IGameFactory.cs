using UnityEngine;

namespace Assets.Scripts.Infracructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        void CreateTile(GameObject at);
        void CreateHud();
        void CreateMainMenu();
        void CreateUIRoot();
        GameObject CreateBgTapButton();
    }
}
