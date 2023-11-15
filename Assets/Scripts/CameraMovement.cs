using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

internal class CameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private List<CinemachineVirtualCamera> _camers;

    private int _currentIndex;
    public event UnityAction<ICinemachineCamera> OnBlendStarted;
    public event UnityAction<ICinemachineCamera> OnBlendFinished;
    
    public event Action Finished;
    
    Coroutine _trackingBlend;
    
    public void MoveNext()
    {
        _currentIndex++;
        
        if(_currentIndex >= _camers.Count)
            return;
        
        _camers[_currentIndex - 1].gameObject.SetActive(false);
        _cinemachineBrain.m_CameraActivatedEvent.AddListener(OnCameraActivated);
      
    }

   private void OnCameraActivated(ICinemachineCamera newCamera, ICinemachineCamera previousCamera)
    {
        if (_trackingBlend != null)
            StopCoroutine(_trackingBlend);
 
        OnBlendStarted?.Invoke(previousCamera);
        _trackingBlend = StartCoroutine(WaitForBlendCompletion(newCamera));
 
       
    }
    
   private IEnumerator WaitForBlendCompletion(ICinemachineCamera newCamera)
    {
        while (_cinemachineBrain.IsBlending)
        {
            yield return null;
        }
 
        OnBlendFinished?.Invoke(newCamera);
        _trackingBlend = null;
    }
    
}