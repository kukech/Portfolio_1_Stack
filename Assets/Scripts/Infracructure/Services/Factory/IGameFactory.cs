using System;
using Assets.Scripts.Infracructure.Logic;
using Assets.Scripts.Infracructure.UI.Windows;
using UnityEngine.UI;

namespace Assets.Scripts.Infracructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        Button BgButton { get; }
        Tower Tower { get; }
        void CreateTower();
        MenuWindow CreateMenu();
        void CreateUIRoot();
        void CreateBgTapButton();
        void CleanUp();
    }
}
