using Assets.Scripts.Infracructure.Services;
using Assets.Scripts.Infracructure.Services.AssetManagement;
using Assets.Scripts.Infracructure.Services.Factory;
using Assets.Scripts.Infracructure.Services.PersistentProgress;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infracructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly AllServices _services;
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(AllServices services, GameStateMachine stateMachine)
        {
            _services = services;
            _stateMachine = stateMachine;

            RegisterServices();
        }

        public void Enter()
        {
            SceneManager.LoadScene(Initial);
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {

        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IProgressService>(new ProgressService());
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IProgressService>()));
        }

    }
}
