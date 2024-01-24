using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sources.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Sources.Logic.StateMachines.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _startCamera;
        [SerializeField] private CinemachineVirtualCamera _cameraToTarget;
        [SerializeField] private CinemachineVirtualCamera _observationCamera;

        private CinemachineBrain _cinemachineBrain;

        private int _currentIndex;

        public event Action Finished;

        Coroutine _trackingBlend;
        private ISaveLoadService _saveLoadService;

        private ICinemachineCamera _previousCamera;

        private void Awake()
        {
            _cinemachineBrain = UnityEngine.Camera.main.gameObject.GetComponent<CinemachineBrain>();
        }

        public void MoveToTarget()
        {
            _startCamera.gameObject.SetActive(false);
            _observationCamera.gameObject.SetActive(false);
            _cameraToTarget.gameObject.SetActive(true);
            
           
            if (_previousCamera != null)
            {
                if (_previousCamera == _cameraToTarget)
                {
                    Finished?.Invoke();
                    return;
                }
            }
            
            _cinemachineBrain.m_CameraActivatedEvent.AddListener(OnCameraActivated);
        }

        public void MoveToStart()
        {
            _observationCamera.gameObject.SetActive(false);
            _cameraToTarget.gameObject.SetActive(false);
            _startCamera.gameObject.SetActive(true);
            _cinemachineBrain.m_CameraActivatedEvent.AddListener(OnCameraActivated);
        }

        public void MoveToObservation()
        {
            _cameraToTarget.gameObject.SetActive(false);
            _startCamera.gameObject.SetActive(false);
            _observationCamera.gameObject.SetActive(true);
            _cinemachineBrain.m_CameraActivatedEvent.AddListener(OnCameraActivated);
        }

        private void OnCameraActivated(ICinemachineCamera newCamera, ICinemachineCamera previousCamera)
        {
            if (_trackingBlend != null)
                StopCoroutine(_trackingBlend);

            _previousCamera = newCamera;
            _trackingBlend = StartCoroutine(WaitForBlendCompletion());
        }

        private IEnumerator WaitForBlendCompletion()
        {
            while (_cinemachineBrain.IsBlending)
            {
                yield return null;
            }

            _trackingBlend = null;
            _cinemachineBrain.m_CameraActivatedEvent.RemoveListener(OnCameraActivated);
            Finished?.Invoke();
        }
    }
}