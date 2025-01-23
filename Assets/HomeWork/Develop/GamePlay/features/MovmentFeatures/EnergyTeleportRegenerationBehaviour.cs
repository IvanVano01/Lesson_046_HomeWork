using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.GamePlay.Entities.Behaviours;
using Assets.HomeWork.Develop.Utils.Conditions;
using Assets.HomeWork.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures
{
    public class EnergyTeleportRegenerationBehaviour : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<float> _teleportationEnergyMax;
        private ReactiveVariable<float> _currentTeleportationEnergy;
        private ReactiveVariable<float> _regenEnergyEveryAmountSeconds;

        private ICondition _condition;

        private float _tenPercentOfEnergyMax;

        public void OnInit(Entity entity)
        {
            _teleportationEnergyMax = entity.GetTeleportationEnergyMax();
            _currentTeleportationEnergy = entity.GetTeleportationEnergy();
            _regenEnergyEveryAmountSeconds = entity.GetRegenEnergyEveryAmountSeconds();

            _condition = entity.GetRegenTeleporEnergyCondition();

            _tenPercentOfEnergyMax = ToCalculatePercentage(_teleportationEnergyMax.Value, 10);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate() == false)
                return;
            
            _currentTeleportationEnergy.Value += _tenPercentOfEnergyMax * (deltaTime / _regenEnergyEveryAmountSeconds.Value);

            if (_currentTeleportationEnergy.Value > _teleportationEnergyMax.Value)
                _currentTeleportationEnergy.Value = _teleportationEnergyMax.Value;

            //Debug.Log($"Востанавливаем энергию для телепорта {_currentTeleportationEnergy.Value}");
        }

        private float ToCalculatePercentage(float Value, float percentOfValue) => (Value / 100) * percentOfValue;
    }
}
