using UnityEngine;

namespace Sources.Logic.StateMachines.Anchor
{
    public class RaycastToTargetState : IState
    {
        private readonly AnchorStateMachine _anchorStateMachine;
        private readonly Raycaster _raycaster;
        private readonly Target.Target _target;

        public RaycastToTargetState(AnchorStateMachine anchorStateMachine, Raycaster raycaster,
            Target.Target target)
        {
            _anchorStateMachine = anchorStateMachine;
            _raycaster = raycaster;
            _target = target;
        }

        public void Enter()
        {
            if (_raycaster.Raycast(_target.FinishedPosition))
                _anchorStateMachine.EnterNextState();
            else
            {
                Debug.Log("Restart level state machine");
                _anchorStateMachine.RestartLevelStateMachine();
            }
        }
    }
}