using System;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private EntryPoint _entryPoint;
        
        private void Awake()
        {
            var entryPoint = FindObjectOfType<EntryPoint>();

            if (entryPoint == null)
            {
                Instantiate(_entryPoint);
            }
        }
    }
}