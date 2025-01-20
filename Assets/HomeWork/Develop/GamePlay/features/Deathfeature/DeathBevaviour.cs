using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Conditions;
using Assets.HomeWork.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.Deathfeature
{
    public class DeathBevaviour : IEntityInitialize, IEntityUpdate
    {
        private ICompositeCondition _condition;
        
        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<bool> _isDeathProcess;



        public void OnInit(Entity entity)
        {            
            _isDead = entity.GetIsDead();

            _condition = entity.GetDeathCondition();

            _isDeathProcess = entity.GetIsDeathProcess();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;

            if (_condition.Evaluate())
            {
                _isDead.Value = true;  
                _isDeathProcess.Value = true;
            }
        }
    }
}
