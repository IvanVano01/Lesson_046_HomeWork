using Assets.HomeWork.Develop.GamePlay.AI.Sensors;
using Assets.HomeWork.Develop.GamePlay.features.MovmentFeatures;
using Assets.HomeWork.Develop.Utils.Conditions;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.Entities.CodeGeneration
{
    public static class EntityExtensionsGenerator// класс автоматической генерации кода
    {
        private static Dictionary<EntityValues, Type> _entityValuesToType = new Dictionary<EntityValues, Type>() // словарь для связки данных"EntityValues" по их типу "Type",
                                                                                                                 // в него будем ручками регистрировать тип данных
        {
            {EntityValues.MoveDirection, typeof(ReactiveVariable<Vector3>)},
            {EntityValues.MoveSpeed, typeof(ReactiveVariable<float>)},

            {EntityValues.MoveToPosition, typeof(MoveToPosition)},
            {EntityValues.MoveCondition, typeof(ICompositeCondition)},// добавляем логическое условие
            {EntityValues.IsMoving, typeof(ReactiveVariable<bool>) }, //логич. усл. Для отображения анимации бега

            {EntityValues.TeleportationRadius, typeof(ReactiveVariable<float>)},
            {EntityValues.TeleportationCenterArea, typeof(ReactiveVariable<Vector2>)},

            {EntityValues.TeleportationEnergyMax, typeof(ReactiveVariable<float>)},
            {EntityValues.TeleportationEnergy, typeof(ReactiveVariable<float>)},
            {EntityValues.RegenEnergyEveryAmountSeconds, typeof(ReactiveVariable<float>)},
            {EntityValues.TeleportationEnergyPrice, typeof(ReactiveVariable<float>)},

            {EntityValues.TryToTeleportEvent, typeof(ReactiveEvent<float>)},
            {EntityValues.GoToTeleportEvent,typeof(ReactiveEvent<Vector3>)},
            {EntityValues.RegenTeleporEnergyCondition, typeof(ICompositeCondition)},
            {EntityValues.TeleportationCondition, typeof(ICompositeCondition)},
            {EntityValues.TeleportationForEnergyCondition, typeof(ICompositeCondition)},
            {EntityValues.RandomGeneratorPosition, typeof(RandomGeneratorPosition)},


            {EntityValues.RotationDirection, typeof(ReactiveVariable<Vector3>)},
            {EntityValues.RotationSpeed, typeof(ReactiveVariable<float>)},
            {EntityValues.RotationCondition, typeof(ICompositeCondition)},// добавляем логическое условие

            {EntityValues.SelfTriggerDamage, typeof(ReactiveVariable<float>)},
            {EntityValues.SelfTriggerReciever, typeof(TriggerReciever)},
            {EntityValues.TeleportationDamage, typeof(ReactiveVariable<float>)},
            {EntityValues.TeleportationDamageRadius, typeof(ReactiveVariable<float>)},

            {EntityValues.CharacterController, typeof(CharacterController)},
            {EntityValues.Transform, typeof(Transform)},

            {EntityValues.Health,typeof(ReactiveVariable<float>)},
            {EntityValues.MaxHealth,typeof(ReactiveVariable<float>)},

            {EntityValues.TakeDamageRequest,typeof(ReactiveEvent<float>)},// реак. событие фильтр()
            {EntityValues.TakeDamageEvent,typeof(ReactiveEvent<float>)}, // реакт, событие о нанесении урона
            {EntityValues.TakeDamageCondition,typeof(ICompositeCondition)},// добавляем логическое условие

            {EntityValues.IsDead,typeof(ReactiveVariable<bool>)},
            {EntityValues.IsDeathProcess,typeof(ReactiveVariable<bool>)},
            {EntityValues.DeathCondition,typeof(ICompositeCondition)},// добавляем логическое условие
            {EntityValues.SelfDestroyCondition,typeof(ICompositeCondition)},

        };

        [InitializeOnLoadMethod]// для обновления компиляции кода
        [MenuItem("Tools/GenerateEntityExtensions")] // обновления кода руками, через панель "Tools"
        private static void Generate()//метод содания файла и записи в него текста кода
        {
            string path = GetPathToExtensionsFile();

            StreamWriter writer = new StreamWriter(path);//библиотека "StreamWriter" для записи файла

            writer.WriteLine(GetClassHeader());//записываем название класса
            writer.WriteLine("{");

            foreach (KeyValuePair<EntityValues, Type> entityValuesToTypePair in _entityValuesToType)
            {
                string type = entityValuesToTypePair.Value.FullName;// "FullName" для добавления "namespace"

                if (entityValuesToTypePair.Value.IsGenericType)// для правильного разпознавания "ReactiveVariable<float>"
                {
                    type = type.Substring(0, type.IndexOf('`'));// выпиливаем "<>" от "ReactiveVariable"

                    type += "<";

                    for (int i = 0; i < entityValuesToTypePair.Value.GenericTypeArguments.Length; i++)// проходимся по аргументам которые в "<>" и берём имя аргумента
                    {
                        type += entityValuesToTypePair.Value.GenericTypeArguments[i].FullName;

                        if (i != entityValuesToTypePair.Value.GenericTypeArguments.Length - 1) // для запятой "," если аргументов несколько
                            type += ",";
                    }

                    type += ">";
                }
                // с пустым конструктором
                if (HasEmptyConstructor(entityValuesToTypePair.Value))
                    writer.WriteLine($"public static {typeof(Entity)} Add{entityValuesToTypePair.Key}(this {typeof(Entity)} entity) => entity.AddValue({typeof(EntityValues)}.{entityValuesToTypePair.Key}, new {type}());");

                // воспроизводим такую запись на примере AddMoveSpeed : "public static Entity AddMoveSpeed(this Entity entity, ReactiveVariable<float> value) => entity.AddValues(EntityValues.MoveSpeed, value);"
                writer.WriteLine($"public static {typeof(Entity)} Add{entityValuesToTypePair.Key}(this {typeof(Entity)} entity, {type} value) => entity.AddValue({typeof(EntityValues)}.{entityValuesToTypePair.Key}, value);");// для "Add"
                writer.WriteLine($"public static {type} Get{entityValuesToTypePair.Key}(this {typeof(Entity)} entity) => entity.GetValue<{type}>({typeof(EntityValues)}.{entityValuesToTypePair.Key});");// для "Get"
                writer.WriteLine($"public static {typeof(bool)} TryGet{entityValuesToTypePair.Key}(this {typeof(Entity)} entity, out {type} value) => entity.TryGetValue<{type}>({typeof(EntityValues)}.{entityValuesToTypePair.Key}, out value);");// для "TryGet"
            }
            writer.WriteLine("}");
            writer.Close();

            AssetDatabase.SaveAssets();// сохранили созданный файл
            AssetDatabase.Refresh();// что бы юнити редактор увидел созданный файл
        }

        private static string GetClassHeader() => "public static class EntityExtensionsGenerated";

        // путь до файла, в который будем записывать сгенерированный код
        private static string GetPathToExtensionsFile() => $"{Application.dataPath}/HomeWork/Develop/GamePlay/Entities/CodeGeneration/EntityExtensionsGenerated.cs";

        // для 
        private static bool HasEmptyConstructor(Type type) =>
            type.IsAbstract == false
            && type.IsSubclassOf(typeof(UnityEngine.Object)) == false
            && type.IsInterface == false
            && type.GetConstructors().Any(constructor => constructor.GetParameters().Count() == 0);
    }
}
