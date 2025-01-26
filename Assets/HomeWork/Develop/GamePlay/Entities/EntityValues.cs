namespace Assets.HomeWork.Develop.GamePlay.Entities
{
    public enum EntityValues// тип данных, которые будем регистрировать
    {
        MoveDirection,
        MoveSpeed,
       
        MoveToPosition,
        MoveCondition,
        IsMoving,

        TeleportationRadius,
        TeleportationCenterArea,

        TeleportationEnergyMax,
        TeleportationEnergy,
        RegenEnergyEveryAmountSeconds,
        TeleportationEnergyPrice,

        TryToTeleportEvent,
        GoToTeleportEvent,
        TeleportationCondition,
        TeleportationForEnergyCondition,
        RegenTeleporEnergyCondition,
        RandomGeneratorPosition,

        RotationDirection,
        RotationSpeed,
        RotationCondition,

        SelfTriggerReciever,
        SelfTriggerDamage,
        TeleportationDamage,
        TeleportationDamageRadius,

        CharacterController,
        Transform,

        Health,
        MaxHealth,

        TakeDamageRequest,
        TakeDamageEvent,
        TakeDamageCondition,

        IsDead,
        IsDeathProcess,
        DeathCondition,
        SelfDestroyCondition,
    }
}
