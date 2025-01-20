using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.HomeWork.Develop.CommonServices.SceneManagment
{
    public interface ISceneLoader
    {
        IEnumerator LoadAsync(SceneID sceneID, LoadSceneMode loadSceneMode = LoadSceneMode.Single);// загружаем сцену в режиме "LoadSceneMode.Single"
    }
}
