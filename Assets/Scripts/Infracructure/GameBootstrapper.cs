using Assets.Scripts.Infracructure.States;
using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Infracructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        public LoadingCurtain CurtainPrefab;
        private Game _game;
        private void Awake()
        {
            _game = new Game(Instantiate(CurtainPrefab));
            _game._stateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}