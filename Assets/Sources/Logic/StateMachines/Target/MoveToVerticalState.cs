using Sources.Logic.StateMachines.Anchor;
using Sources.Logic.StateMachines.Camera;
using Sources.Services.Input;

namespace Sources.Logic.StateMachines.Target
{
    public class MoveToVerticalState : IState
    {
        private readonly TargetStateMachine _targetStateMachine;
        private readonly Target _target;
        private readonly IInputService _inputService;

        public MoveToVerticalState(TargetStateMachine targetStateMachine, Target target, IInputService inputService)
        {
            _targetStateMachine = targetStateMachine;
            _target = target;
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.Enable();
            _inputService.Clicked += OnClicked;
            _target.MoveToVertical();
        }

        private void OnClicked()
        {
            _inputService.Clicked -= OnClicked;
            _inputService.Disable();
            _target.Disable();
            _targetStateMachine.EnterStateMachine<AnchorStateMachine>();
        }
    }
}