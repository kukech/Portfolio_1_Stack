using Assets.Scripts.Infracructure.Services;

namespace Assets.Scripts.Infracructure.States
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : IState;
    }
}