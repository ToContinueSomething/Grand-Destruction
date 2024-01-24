using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string nameScene, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(nameScene, onLoaded));

        private IEnumerator LoadScene(string nameScene, Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nameScene);

            while (waitNextScene.isDone == false)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}