using Assets.HomeWork.Develop.CommonServices.AssetManagment;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.GamePlay.features.DamageFeature;
using Assets.HomeWork.Develop.GamePlay.features.Deathfeature;
using Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures;
using Assets.HomeWork.Develop.Utils.Conditions;
using Assets.HomeWork.Develop.Utils.Extensions;
using Assets.HomeWork.Develop.Utils.Reactive;
using UnityEngine;


namespace Assets.HomeWork.Develop.GamePlay.Entities
{
    public class EntityFactory // общая фабрика по созданию сущностей "Entity" и наполнению данными, состояниями и т.д
    {
        private string GhostPrefabPath = "GamePlay/Creatures/Ghost";

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
            //------------------------запись без автогенерации кода----------------------------//
            //.AddValue(EntityValues.MoveDirection, new ReactiveVariable<Vector3>())
            //.AddMoveSpeed(new ReactiveVariable<float>())                

            //.AddValue(EntityValues.RotationDirection, new ReactiveVariable<Vector3>())
            //.AddValue(EntityValues.RotationSpeed, new ReactiveVariable<float>(900));                
            //-----------------------------------------------------------------------------------//
            .AddMoveDirection()
            .AddMoveSpeed(new ReactiveVariable<float>(10))
            .AddIsMoving()

            .AddRotationDirection()
            .AddRotationSpeed(new ReactiveVariable<float>(900))

            .AddHealth(new ReactiveVariable<float>(800))
            .AddMaxHealth(new ReactiveVariable<float>(800))
            .AddTakeDamageRequest()
            .AddTakeDamageEvent()
            .AddIsDead()
            .AddIsDeathProcess()
            .AddSelfTriggerDamage(new ReactiveVariable<float>(150));

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
    }
}
