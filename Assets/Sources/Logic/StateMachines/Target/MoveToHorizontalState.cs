using Sources.Services.Input;
using UnityEngine;

namespace Sources.Logic.StateMachines.Target
{
    public class MoveToHorizontalState : IState
    {
        private readonly TargetStateMachine _targetStateMachine;
        private readonly Target _target;
        private readonly IInputService _inputService;

        public MoveToHorizontalState(TargetStateMachine targetStateMachine, Target target, IInputService inputService)
        {
            _targetStateMachine = targetStateMachine;
            _target = target;
            _inputService = inputService;
        }

        public void Enter()
        {
            _target.gameObject.SetActive(true);
            _inputService.Enable();
            _target.MoveToHorizontal();
            _inputService.Clicked += OnClicked;
        }

        private void OnClicked()
        {
            _inputService.Clicked -= OnClicked;
            _targetStateMachine.EnterNextState();
        }
    }
}