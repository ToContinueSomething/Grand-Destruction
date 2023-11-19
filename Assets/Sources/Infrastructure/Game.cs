using Sources.Infrastructure.Services;
using Sources.Infrastructure.States;
using Sources.Logic;

namespace Sources.Infrastructure
{
    public class Game
    {
        public GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, LevelMapLoader loaderLevelMap)
        {
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner),loadingCurtain, AllServices.Container, loaderLevelMap);
        }
    }
}