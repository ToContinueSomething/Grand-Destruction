using Sources.Logic.StateMachines.Target;
using UnityEngine;

namespace Sources.Logic.StateMachines.Camera
{
    public class MoveToTargetState : IState
    {
        private readonly CameraStateMachine _cameraStateMachine;
        private readonly CameraMovement _cameraMovement;

        public MoveToTargetState(CameraStateMachine cameraStateMachine, CameraMovement cameraMovement)
        {
            _cameraStateMachine = cameraStateMachine;
            _cameraMovement = cameraMovement;
        }
        
        public void Enter()
        {
            _cameraMovement.Finished += OnFinished;
            _cameraMovement.MoveToTarget();
        }

        private void OnFinished()
        {
            Debug.Log("Next State");
            _cameraMovement.Finished -= OnFinished;
            _cameraStateMachine.EnterStateMachine<TargetStateMachine>();
        }
    }
}