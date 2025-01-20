using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Conditions;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.Deathfeature
{
    public class SelfDestroyBehaviour : IEntityInitialize, IEntityUpdate 
    {
        private ICondition _selfDestroyCondition;
        private Transform _entityTransform;

        public void OnInit(Entity entity)
        {
            _entityTransform = entity.GetTransform();
            _selfDestroyCondition =entity.GetSelfDestroyCondition();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_selfDestroyCondition.Evaluate())
            Object.Destroy(_entityTransform.gameObject);
        }
    }
}
