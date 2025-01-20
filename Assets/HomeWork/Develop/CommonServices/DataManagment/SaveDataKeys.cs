using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public static class SaveDataKeys // статический класс сохранения ключей, по которым будут запрашиваться данные
    {
        private static Dictionary<Type, string> Keys = new Dictionary<Type, string>() // словарь для хранения ключей,
                                                                                      // сам ключ будет в формате"string"
        {
            {typeof(PlayerData), "PlayerData" }, // сохранили класс с данными для игрока                    
        };

        public static string GetKeyFor<TData>() where TData : ISaveData //метод получения ключа для запрашиваемого типа данных
            => Keys[typeof(TData)];                                     // в качастве типа запрашиваемых данных, могут быть только типы(классы)
                                                                        // унаследованные от интерфейса "ISaveData"
    }
}
