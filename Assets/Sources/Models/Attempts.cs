using System;
using Sources.Data;
using Sources.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Sources.Models
{
    public class Attempts : MonoBehaviour , ISavedProgressReader
    {
        private int _value;

        public int Value => _value;
        
        public event Action<int> ValueChanged;

        public void LoadProgress(PlayerProgress progress)
        {
            _value = progress.AnchorStats.Attempts;
            ValueChanged?.Invoke(_value);
        }

        public void Reduce()
        {
            _value--;
            ValueChanged?.Invoke(_value);
        }
    }
}