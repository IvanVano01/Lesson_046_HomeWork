using Assets.HomeWork.Develop.CommonServices.LevelsService;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using UnityEngine;

namespace Assets.HomeWork.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelTilePresenter
    {
        private const int FirstLevel = 1;
        // модель
        private readonly CompletedLevelsService _levelsService;
        private SceneSwitcher _sceneSwitcher;

        private readonly int _levelNumber;
        private bool _isBlocked;// маркер, заблокирован уровень или нет

        //вью
        private LevelTileView _view;

        public LevelTilePresenter(
            CompletedLevelsService levelsService,
            SceneSwitcher sceneSwitcher,
            int levelNumber,
            LevelTileView view)
        {
            _levelsService = levelsService;
            _sceneSwitcher = sceneSwitcher;
            _levelNumber = levelNumber;
            _view = view;
        }

        public LevelTileView View => _view;// вьюху на чтение

        public void Enable()
        {
            _isBlocked = _levelNumber != FirstLevel && PreviousLevelCompleted() == false;// уровень заблокирован,если он не первый
                                                                                         // и если предыдущий ещё не пройден
            _view.SetLevel(_levelNumber.ToString());

            if (_isBlocked)
            {
                _view.SetBlock();
            }
            else
            {
                if (_levelsService.IsLevelCompleted(_levelNumber))
                    _view.SetComplete();
                else
                    _view.SetActive();
            }

            _view.Cliked += OnViewCliked;
        }

        public void Disable()
        {
            _view.Cliked -= OnViewCliked;
        }

        private void OnViewCliked()
        {
            if (_isBlocked)
            {
                // выводим сообщение на "Popup" что уровень заблокирован
                Debug.Log(" Уровень заблокирован, пройдите предыдущий! ");
                    return;
            }

            if (_levelsService.IsLevelCompleted(_levelNumber))
            {
                Debug.Log(" Уровень завершён! ");
                return;
            }

            _sceneSwitcher.ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(_levelNumber)));
        }

        private bool PreviousLevelCompleted() => _levelsService.IsLevelCompleted(_levelNumber - 1);
    }
}
