namespace Sources.Logic.StateMachines.Anchor
{
    public class MoveState : IState
    {
        private readonly AnchorStateMachine _anchorStateMachine;
        private readonly Anchor _anchor;

        public MoveState(AnchorStateMachine anchorStateMachine, Anchor anchor)
        {
            _anchorStateMachine = anchorStateMachine;
            _anchor = anchor;
        }

        public void Enter()
        {
            _anchor.Move();
            _anchor.Finished += OnFinished;
        }

        private void OnFinished()
        {
            _anchor.Finished -= OnFinished;
            _anchorStateMachine.EnterNextState();
        }
    }
}