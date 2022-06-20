using UnityEngine;

namespace Assets.Scripts.Infracructure.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 at);
    }
}