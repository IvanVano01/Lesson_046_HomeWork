using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.HomeWork.Develop.Configs.GamePlay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelListConfig", fileName = "LevelListConfig")]
    public class LevelListConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levels;

        public IReadOnlyList<LevelConfig> Levels => _levels;

        public LevelConfig GetBy(int level)
        {
            int levelIndex = level - 1;

            if(level >= _levels.Count)// если номер запрашиваемого уровня больше чем существует уровней
                //return _levels.Last(); // то выдаём номер последнего ур.
                throw new ArgumentOutOfRangeException(nameof(level));

            return _levels[levelIndex];
        }
    }
}
