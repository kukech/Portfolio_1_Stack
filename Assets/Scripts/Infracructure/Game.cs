using Assets.Scripts.Infracructure.Services;
using Assets.Scripts.Infracructure.States;
using Assets.Scripts.Logic;

namespace Assets.Scripts.Infracructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public Game(LoadingCurtain curtain)
        {
            _stateMachine = new GameStateMachine(curtain, AllServices.Container);
        }
    }
}