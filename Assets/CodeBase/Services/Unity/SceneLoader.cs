using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.Unity
{
    public class SceneLoader : ISceneLoader
    {
        private ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void LoadAsync(string sceneName, Action onLoaded) => 
            _coroutineRunner.StartCoroutine(LoadSceneAsync(sceneName, onLoaded));

        public void LoadStraight(string sceneName) => 
            LoadSceneStraight(sceneName);

        private IEnumerator LoadSceneAsync(string sceneName, Action action)
        {
            AsyncOperation newScene = SceneManager.LoadSceneAsync(sceneName);

            while (!newScene.isDone)
                yield return null;

            action?.Invoke();
        }
        
        private void LoadSceneStraight(string sceneName)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
                return;

            SceneManager.LoadScene(sceneName);
        }
    }
}