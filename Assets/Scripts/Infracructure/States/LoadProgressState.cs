using Assets.Scripts.Infracructure.Services.PersistentProgress;

namespace Assets.Scripts.Infracructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IProgressService _progressService;

        public LoadProgressState(
            GameStateMachine gameStateMachine, 
            IProgressService progressService)
        {
            _stateMachine = gameStateMachine;
            _progressService = progressService;
        }

        public void Enter()
        {
            LoadProgress();
            _stateMachine.Enter<LoadGameState>();
        }

        public void Exit()
        {
        }

        private void LoadProgress() =>
            _progressService.LoadProgress();
    }
}