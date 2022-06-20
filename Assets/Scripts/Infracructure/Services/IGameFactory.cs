using UnityEngine;

namespace Assets.Scripts.Infracructure.Services
{
    public interface IGameFactory : IService
    {
        public GameObject CreateTile(Vector3 at);
    }
}
