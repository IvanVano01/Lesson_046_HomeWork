using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.HomeWork.Develop.CommonServices.SceneManagment
{
    public class DefaultSceneLoader : ISceneLoader
    {
        public IEnumerator LoadAsync(SceneID sceneID, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            AsyncOperation waitLoading = SceneManager.LoadSceneAsync(sceneID.ToString(), loadSceneMode);// способ загрузки асинхронный

            while (waitLoading.isDone == false)// корутина не завершится пока не загрузит сцену
                yield return null;
        }
    }
}
