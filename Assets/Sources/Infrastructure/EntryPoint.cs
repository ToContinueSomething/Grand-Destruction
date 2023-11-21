using Sources.Infrastructure.States;
using Sources.Logic;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtain;

        private void Awake()
        {
            Game game = new Game(this, Instantiate(_curtain));
            game.GameStateMachine.Enter<EntryState>();
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(_curtain);
        }
    }
}