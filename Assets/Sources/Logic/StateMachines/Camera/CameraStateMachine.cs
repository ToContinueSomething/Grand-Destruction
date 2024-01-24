using System.Collections.Generic;

namespace Sources.Logic.StateMachines.Camera
{
    public class CameraStateMachine : IStateMachine
    {
        private readonly List<IState> _states;
        private readonly LevelStateMachine _levelLevelStateMachine;
        
        private int _currentIndex;

        public CameraStateMachine(CameraMovement cameraMovement, LevelStateMachine levelLevelStateMachine)
        {
            _levelLevelStateMachine = levelLevelStateMachine;
            _states = new List<IState>
            {
                new MoveToStartState(this,cameraMovement),
                new MoveToTargetState(this,cameraMovement),
                new MoveToObservationState(this,cameraMovement)
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
            _currentIndex = 0;
        }
    }
}