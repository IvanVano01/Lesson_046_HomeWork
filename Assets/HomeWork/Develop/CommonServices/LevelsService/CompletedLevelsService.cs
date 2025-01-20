using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.LevelsService
{
    public class CompletedLevelsService : IDataReader<PlayerData>, IDataWrite<PlayerData> 
    {
        private List<int> _completedLevels = new();// список пройденных уровней       

        public CompletedLevelsService(PlayerDataProvider playerDataProvider)
        {          
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public bool IsLevelCompleted(int levelNumber) => _completedLevels.Contains(levelNumber);

        public bool TryAddLevelToCompleted(int levelNumber)
        {
            if (IsLevelCompleted(levelNumber))
                return false;

            _completedLevels.Add(levelNumber);
            return true;
        }

        public void ReadFrom(PlayerData data)// берём из даты
        {
            _completedLevels.Clear();
            _completedLevels.AddRange(data.CompletedLevels);
        }

        public void WriteTo(PlayerData data)// записываем в дату
        {
            data.CompletedLevels.Clear();
            data.CompletedLevels.AddRange(_completedLevels);
        }
    }
}
