using Assets.Scripts.Infracructure.Services.AssetManagement;
using UnityEngine;

namespace Assets.Scripts.Infracructure.Services
{
    public class GameFactory : IGameFactory
    {

        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateTile(Vector3 at)
        {
            return _assetProvider.Instantiate(AssetPath.TilePath, at: at);
        }
    }
}
