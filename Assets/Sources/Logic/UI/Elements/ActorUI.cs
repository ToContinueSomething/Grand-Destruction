using System;
using Sources.Models;
using UnityEngine;

namespace Sources.Logic.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private AttemptsView _attemptsView;

        private Attempts _attempts;

        public void Construct(Attempts attempts)
        {
            _attempts = attempts;
            _attempts.ValueChanged += OnAttemptsValueChanged;
        }

        private void OnAttemptsValueChanged(int value)
        {
            _attemptsView.SetValue(value);
        }

        private void OnDestroy()
        {
            if (_attempts != null)
                _attempts.ValueChanged -= OnAttemptsValueChanged;
        }
    }
}