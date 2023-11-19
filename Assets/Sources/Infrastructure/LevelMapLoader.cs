using System;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class LevelMapLoader : MonoBehaviour
    {
        public void Load(Action<string> onFinished)
        {
            onFinished?.Invoke("level_1");
        }
    }
}