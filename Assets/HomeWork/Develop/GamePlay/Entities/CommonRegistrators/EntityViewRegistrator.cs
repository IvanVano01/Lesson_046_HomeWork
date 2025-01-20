using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.Entities.CommonRegistrators
{
    public class EntityViewRegistrator : MonoEntityRegistrator// клас будет регистрировать вюю сущности, унаследованные от монобеха
    {
        [SerializeField] private GameObject _rootView;//контейнер вьюхи,

        public override void Register(Entity entity)
        {
            foreach(EntityView entityView in _rootView.GetComponentsInChildren<EntityView>())// регистрируем дочерние сущности"Entity"
                                                                                             // в нутри контёйнера"_rootView"
            {
                entityView.SubscribeTo(entity);
            }
        }
    }
}
