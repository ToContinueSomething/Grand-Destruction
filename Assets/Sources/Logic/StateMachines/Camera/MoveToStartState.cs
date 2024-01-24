using Sources.Logic.StateMachines.Target;
using UnityEngine;

namespace Sources.Logic.StateMachines.Camera
{
    public class MoveToStartState : IState
    {
        private readonly CameraStateMachine _cameraStateMachine;
        private readonly CameraMovement _cameraMovement;

        public MoveToStartState(CameraStateMachine cameraStateMachine, CameraMovement cameraMovement)
        {
            _cameraStateMachine = cameraStateMachine;
            _cameraMovement = cameraMovement;
        }
        
        public void Enter()
        {
            _cameraMovement.Finished += OnFinished;
            _cameraMovement.MoveToStart();
        }

        private void OnFinished()
        {
            _cameraMovement.Finished -= OnFinished;
            _cameraStateMachine.EnterNextState();
        }
    }
}