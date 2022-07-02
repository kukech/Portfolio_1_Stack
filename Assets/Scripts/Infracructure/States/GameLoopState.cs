using Assets.Scripts.Infracructure.Services.Factory;

namespace Assets.Scripts.Infracructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;

        public GameLoopState(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Enter()
        {

        }

        public void Exit()
        {
            _gameFactory.CleanUp();
        }
    }
}