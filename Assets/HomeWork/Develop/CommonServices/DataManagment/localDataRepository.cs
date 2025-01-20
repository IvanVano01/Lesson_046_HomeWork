using System.IO;
using UnityEngine;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    internal class localDataRepository : IDataRepository // репозиторий, локальное хранилище данных
    {
        private const string SaveFileExtension = "json"; // константа для типа файла, т.е его расширение(json)

        private string FolderPath => Application.persistentDataPath;  // путь к файлу "json"

        public bool Exists(string key) => File.Exists(FullPathFor(key));// проверяем, есть ли такой сохранённый файл


        public string Read(string key) => File.ReadAllText(FullPathFor(key));// возвращает нужный нам файл(key - это название файла)


        public void Remove(string key) => File.Delete(FullPathFor(key));// удаляем файл


        public void Write(string key, string serializedData)
            => File.WriteAllText(FullPathFor(key), serializedData);  // записываем в файл, если файла нет, то он будет создан       

        private string FullPathFor(string key) => Path.Combine(FolderPath, key) + "." + SaveFileExtension;// полный путь к файлу "json"
    }
}
