using System;
using System.Collections.Generic;
using Assets.Scripts.Infracructure.Services;
using Assets.Scripts.Logic;

namespace Assets.Scripts.Infracructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(services, this),
                [typeof(LoadGameState)] = new LoadGameState(this, services.Single<IGameFactory>(), curtain),
                [typeof(GameLoopState)] = new GameLoopState()
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();

            IState state = _states[typeof(TState)];
            _activeState = state;
            state.Enter();
        }
    }
}
