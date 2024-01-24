using System.Collections.Generic;
using UnityEngine;

namespace Sources.Logic.StateMachines.Anchor
{
    public class AnchorStateMachine : IStateMachine
    {
        private readonly List<IState> _states;
        private readonly LevelStateMachine _levelLevelStateMachine;

        private int _currentIndex;

        public AnchorStateMachine(Anchor anchor, Target.Target target, LevelStateMachine levelLevelStateMachine,
            Entity entity, GameFinish gameFinish)
        {
            _levelLevelStateMachine = levelLevelStateMachine;
            _states = new List<IState>
            {
                new RaycastToTargetState(this,anchor.Raycaster,target),
                new SetPositionState(this, anchor, anchor.Raycaster),
                new MoveState(this, anchor),
                new FinishState(this, anchor,entity,gameFinish)
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

        public void RestartLevelStateMachine()
        {
            _levelLevelStateMachine.RestartStateMachines();
        }
    }
}