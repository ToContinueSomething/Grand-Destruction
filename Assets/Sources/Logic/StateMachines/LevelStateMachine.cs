using System;
using System.Collections.Generic;
using Sources.Logic.StateMachines.Anchor;
using Sources.Logic.StateMachines.Camera;
using Sources.Logic.StateMachines.Target;
using Sources.Services.Input;

namespace Sources.Logic.StateMachines
{
    public class LevelStateMachine
    {
        private readonly Dictionary<Type, IStateMachine> _stateMachines;

        public LevelStateMachine(Target.Target target, CameraMovement cameraMovement, Anchor.Anchor anchor,
            IInputService inputService, Entity entity, GameFinish gameFinish)
        {
            _stateMachines = new Dictionary<Type, IStateMachine>()
            {
                [typeof(CameraStateMachine)] = new CameraStateMachine(cameraMovement, this),
                [typeof(TargetStateMachine)] = new TargetStateMachine(target, inputService, this),
                [typeof(AnchorStateMachine)] = new AnchorStateMachine(anchor, target, this, entity,gameFinish)
            };
        }

        public void EnterStateMachine<TStateMachine>() where TStateMachine : IStateMachine
        {
            var stateMachine = _stateMachines[typeof(TStateMachine)];
            stateMachine.EnterNextState();
        }

        public void RestartStateMachines()
        {
            foreach (var stateMachine in _stateMachines)
            {
                stateMachine.Value.Restart();
            }

            EnterStateMachine<CameraStateMachine>();
        }
    }
}