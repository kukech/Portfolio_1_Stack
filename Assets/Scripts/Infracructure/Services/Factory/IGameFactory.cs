using System;
using Assets.Scripts.Infracructure.UI.Windows;
using UnityEngine;

namespace Assets.Scripts.Infracructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        event Action SuccessDrop;
        void CreateTile();
        MenuWindow CreateMenu();
        void CreateUIRoot();
        void CreateBgTapButton(MenuWindow menuWindow);
        void DropTile();
        void CleanUp();
        void InitializeTower();
    }
}
