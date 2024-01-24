using System.Collections.Generic;
using Sources.Services.Input;

namespace Sources.Logic.StateMachines.Target
{
    public class TargetStateMachine : IStateMachine
    {
        private readonly List<IState> _states;
        private readonly LevelStateMachine _levelLevelStateMachine;
        private int _currentIndex;

        public TargetStateMachine(Target target,IInputService inputService,LevelStateMachine levelLevelStateMachine)
        {
            _levelLevelStateMachine = levelLevelStateMachine;
            _states = new List<IState>
            {
                new MoveToHorizontalState(this,target,inputService),
                new MoveToVerticalState(this,target,inputService),
            };

            _currentIndex = -1;
        }

        public void EnterStateMachine<TStateMachine>() where TStateMachine : IStateMachine
        {
            _levelLevelStateMachine.EnterStateMachine<TStateMachine>();
        }

        public void EnterNextState()
        {
            _currentIndex++;
            _states[_currentIndex].Enter();
        }

        public void Restart()
        {
            _currentIndex = -1;
        }
    }
}