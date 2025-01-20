using UnityEngine;

namespace Assets.HomeWork.Develop.CommonServices.LoadingScreen
{
    public class StandartLoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        public bool IsShow => gameObject.activeSelf;

        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
        }

        public void Hide() => gameObject.SetActive(false);        

        public void Show() => gameObject.SetActive(true);        
    }
}
