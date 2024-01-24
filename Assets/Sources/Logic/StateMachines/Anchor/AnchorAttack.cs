using System;
using Sources.Data;
using Sources.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Sources.Logic.StateMachines.Anchor
{
    public class AnchorAttack : MonoBehaviour, ISavedProgressReader
    {
        private Stats _stats;
        
        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.AnchorStats;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Cube cube)) 
                cube.Detouch();
        }
    }
}
