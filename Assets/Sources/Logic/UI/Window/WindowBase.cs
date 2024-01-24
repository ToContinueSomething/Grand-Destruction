using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Logic.UI.Window
{
    public abstract class WindowBase : MonoBehaviour
    {
        private void OnEnable()
        {
            Enable();
        }

        private void OnDisable()
        {
            Disable();
        }
        
        protected abstract void Enable();
        protected abstract void Disable();
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}