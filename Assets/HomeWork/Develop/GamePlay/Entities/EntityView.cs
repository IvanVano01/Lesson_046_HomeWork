using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.Entities
{
    public abstract class EntityView : MonoBehaviour // базовый клас для отображения сущностей
    {
        public  void SubscribeTo(Entity entity)// метод подписки на ту сущность которую хотим отобразить
        {
            entity.Initialized += OnEntityInitialized;
            entity.Disposed += OnEntityDisposed;
        }

        protected virtual void OnEntityDisposed(Entity entity)// метод отписки
        {
            entity.Disposed -= OnEntityDisposed;
            entity.Initialized -= OnEntityInitialized;
        }

        protected abstract void OnEntityInitialized(Entity entity);        
    }
}
