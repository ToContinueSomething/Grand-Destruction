using DG.Tweening;
using UnityEngine;

namespace Sources.Logic.StateMachines.Anchor
{
    public class FinishState : IState
    {
        private readonly AnchorStateMachine _anchorStateMachine;
        private readonly Anchor _anchor;
        private readonly Entity _entity;
        private readonly GameFinish _gameFinish;

        public FinishState(AnchorStateMachine anchorStateMachine, Anchor anchor, Entity entity, GameFinish gameFinish)
        {
            _anchorStateMachine = anchorStateMachine;
            _anchor = anchor;
            _entity = entity;
            _gameFinish = gameFinish;
        }

        public void Enter()
        {
            _anchor.ReduceAttempts();

            Debug.Log(_entity.CountCubes);
            
            if (_entity.IsDestroy)
            {
                DOTween.KillAll();
                _gameFinish.Finish(true);
            }
            else
            {
                if (_anchor.CanAttack)
                {
                    _anchor.Disable();
                    _anchorStateMachine.RestartLevelStateMachine();
                }
                else
                {
                    DOTween.KillAll();
                    _gameFinish.Finish(false);
                }
            }
        }
    }
}