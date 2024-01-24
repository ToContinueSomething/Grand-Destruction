using Sources.Logic.StateMachines.Anchor;
using UnityEngine;

namespace Sources.Logic.StateMachines.Camera
{
    public class MoveToObservationState : IState
    {
        private readonly CameraStateMachine _cameraStateMachine;
        private readonly CameraMovement _cameraMovement;

        public MoveToObservationState(CameraStateMachine cameraStateMachine, CameraMovement cameraMovement)
        {
            _cameraStateMachine = cameraStateMachine;
            _cameraMovement = cameraMovement;
        }

        public void Enter()
        {
            _cameraMovement.Finished += OnFinished;
            _cameraMovement.MoveToObservation();
        }

        private void OnFinished()
        {
            _cameraMovement.Finished -= OnFinished;
            _cameraStateMachine.EnterStateMachine<AnchorStateMachine>();
        }
    }
}