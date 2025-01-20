using Assets.HomeWork.Develop.Configs.GamePlay;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelTileListpresenter
    {
        // модель
        private readonly LevelListConfig _leveListConfig;
        private readonly LevelsMenuPopupFactory _levelsMenuPopupFactory;

        private List<LevelTilePresenter> _levelTilesPresenters = new();
        
        //вью
        private readonly LevelTileListView _view;

        public LevelTileListpresenter(
            LevelListConfig leveListConfig,
            LevelsMenuPopupFactory levelsMenuPopupFactory,
            LevelTileListView view)
        {
            _leveListConfig = leveListConfig;
            _levelsMenuPopupFactory = levelsMenuPopupFactory;
            _view = view;
        }

        public void Enable()
        {
            for (int i = 0; i < _leveListConfig.Levels.Count; i++)
            {
                LevelTileView levelTileView = _view.SpawnElement();
                LevelTilePresenter levelTilePresenter = _levelsMenuPopupFactory.CreateLevelTilePresenter(levelTileView, i + 1);
                levelTilePresenter.Enable();

                _levelTilesPresenters.Add(levelTilePresenter);
            }
        }

        public void Disable()
        {
            foreach( LevelTilePresenter levelTilePresenter in _levelTilesPresenters)
            {
                levelTilePresenter.Disable();
                _view.Remove(levelTilePresenter.View);
            }

            _levelTilesPresenters.Clear();
        }
    }
}
