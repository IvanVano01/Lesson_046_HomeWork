using Assets.HomeWork.Develop.CommonServices.AssetManagment;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.GamePlay.features.DamageFeature;
using Assets.HomeWork.Develop.GamePlay.features.Deathfeature;
using Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures;
using Assets.HomeWork.Develop.Utils.Conditions;
using Assets.HomeWork.Develop.Utils.Reactive;
using UnityEngine;


namespace Assets.HomeWork.Develop.GamePlay.Entities
{
    public class EntityFactory // общая фабрика по созданию сущностей "Entity" и наполнению данными, состояниями и т.д
    {
        private string GhostPrefabPath = "GamePlay/Creatures/Ghost";
        private string SoulMagePrefabPath = "GamePlay/Creatures/SoulMage";

        private DIContainer _container;
        private ResourcesAssetLoader _assets;

        public EntityFactory(DIContainer container)
        {
            _container = container;
            _assets = _container.Resolve<ResourcesAssetLoader>();
        }

        public Entity CreateGhost(Vector3 position)// метод создания сущности призрака"Ghost"
        {
            // создаём сущность
            Entity prefab = _assets.LoadResource<Entity>(GhostPrefabPath);
            Entity instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            // наполняем сущность данными
            instance

            .AddMoveDirection()
            .AddMoveSpeed(new ReactiveVariable<float>(10))
            .AddIsMoving()

            .AddRotationDirection()
            .AddRotationSpeed(new ReactiveVariable<float>(900))

            .AddHealth(new ReactiveVariable<float>(600))
            .AddMaxHealth(new ReactiveVariable<float>(600))
            .AddTakeDamageRequest()
            .AddTakeDamageEvent()
            .AddIsDead()
            .AddIsDeathProcess()
            .AddSelfTriggerDamage(new ReactiveVariable<float>(200));

            // формируем логическое условия 
            ICompositeCondition deathCondition = new CompositeCondition(LogicOperations.AndOperation)
                 .Add(new FuncCondition(() => instance.GetHealth().Value <= 0));

            ICompositeCondition takeDamageCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false)); // пока живы, можем получать урон

            // создаём экземпляр"new CompositeCondition(в него передаём статический класс"LogicOperations.AndOperation")"
            //у созданного эеземпляра вызываем метод "Add(в него передаём новый экземпляр класса "new FuncCondition(сюда передаём метод() => bool == false)")"
            ICompositeCondition moveCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));// если мы не мёртвые, значит можем двигаться

            ICompositeCondition rotationCondition = new CompositeCondition(LogicOperations.AndOperation)
               .Add(new FuncCondition(() => instance.GetIsDead().Value == false));

            ICompositeCondition selfDestroyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == true))// если мы мёртвые, значит можем удалить себя
                .Add(new FuncCondition(() => instance.GetIsDeathProcess().Value == false));// если анимация смерти закончилась,значит можем удалить себя

            // регестрируем у сущности логические условия
            instance
            .AddMoveCondition(moveCondition)
            .AddRotationCondition(rotationCondition)
            .AddDeathCondition(deathCondition)
            .AddTakeDamageCondition(takeDamageCondition)
            .AddSelfDestroyCondition(selfDestroyCondition);

            // добавляем к сущности состояния
            instance
                .AddBehaviour(new CharacterControllerMovmentBehaviour())
                .AddBehaviour(new RotationBehaviour())
                .AddBehaviour(new ApplyDamageFillterBehaviour())
                .AddBehaviour(new ApplyDamageBehaviour())
                .AddBehaviour(new DealDamageOnSelfTriggerBehaviour())
                .AddBehaviour(new DeathBevaviour())
                .AddBehaviour(new SelfDestroyBehaviour());

            instance.Initialize();

            return instance;
        }

        public Entity CreateSoulMage(Vector3 position)
        {
            // спавним префаб сущности

            Entity prefab = _assets.LoadResource<Entity>(SoulMagePrefabPath);
            Entity instance = GameObject.Instantiate(prefab, position, Quaternion.identity, null);

            // наполняем сущность данными
            instance
            
            .AddRotationDirection()
            .AddMoveToPosition(new MoveToPosition())
            .AddIsMoving()
            .AddRotationSpeed(new ReactiveVariable<float>(900))

            .AddTeleportationRadius(new ReactiveVariable<float>(5))
            .AddTeleportationCenterArea(new ReactiveVariable<Vector2>(Vector2.zero))

            .AddTeleportationEnergyMax(new ReactiveVariable<float>(100))
            .AddTeleportationEnergy(new ReactiveVariable<float>(100))
            .AddRegenEnergyEveryAmountSeconds(new ReactiveVariable<float>(1))
            .AddTeleportationEnergyPrice(new ReactiveVariable<float>(15))

            .AddTryToTeleportEvent()
            .AddGoToTeleportEvent()
            .AddRandomGeneratorPosition(new RandomGeneratorPosition())

            .AddHealth(new ReactiveVariable<float>(800))
            .AddMaxHealth(new ReactiveVariable<float>(800))
            .AddTakeDamageRequest()
            .AddTakeDamageEvent()
            .AddIsDead()
            .AddIsDeathProcess()
            .AddSelfTriggerDamage(new ReactiveVariable<float>(200));




            // формируем логическое условия
            ICompositeCondition deathCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetHealth().Value <= 0));

            ICompositeCondition takeDamageCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false)); // пока живы, можем получать урон 

            ICompositeCondition moveCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));// если мы не мёртвые, значит можем двигаться

            ICompositeCondition teleportationCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetTeleportationEnergy().Value >= instance.GetTeleportationEnergyPrice().Value)) // пока есть енергия, можем телепортироваться
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false)); // пока живы, можем телепортироваться

            ICompositeCondition regenTeleporEnergyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetTeleportationEnergy().Value < instance.GetTeleportationEnergyMax().Value));

            ICompositeCondition rotationCondition = new CompositeCondition(LogicOperations.AndOperation)
               .Add(new FuncCondition(() => instance.GetIsDead().Value == false));// пока живы, можем крутиться(вращаться)

            ICompositeCondition selfDestroyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == true))// если мы мёртвые, значит можем удалить себя
                .Add(new FuncCondition(() => instance.GetIsDeathProcess().Value == false));// если анимация смерти закончилась,значит можем удалить себя



            // регестрируем у сущности логические условия
            instance
            .AddMoveCondition(moveCondition)
            .AddRotationCondition(rotationCondition)
            .AddTeleportationCondition(teleportationCondition)
            .AddRegenTeleporEnergyCondition(regenTeleporEnergyCondition)
            .AddDeathCondition(deathCondition)
            .AddTakeDamageCondition(takeDamageCondition)
            .AddSelfDestroyCondition(selfDestroyCondition);

            // добавляем к сущности состояния
            instance
            .AddBehaviour(new RotationBehaviour())
            .AddBehaviour(new TeleportatoinBehaviour())
            .AddBehaviour(new EnergyTeleportRegenerationBehaviour())
            .AddBehaviour(new ApplyDamageFillterBehaviour())
            .AddBehaviour(new ApplyDamageBehaviour())
            .AddBehaviour(new DealDamageOnSelfTriggerBehaviour())
            .AddBehaviour(new DeathBevaviour())
            .AddBehaviour(new SelfDestroyBehaviour());


            instance.Initialize();
            return instance;
        }
    }
}
