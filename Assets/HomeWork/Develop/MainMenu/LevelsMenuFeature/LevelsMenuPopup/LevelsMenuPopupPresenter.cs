using System;

namespace Assets.HomeWork.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelsMenuPopupPresenter
    {
        private const string TitleName = "Levels";
        //модель
        private readonly LevelsMenuPopupFactory _levelsMenuPopupFactory;
        private LevelTileListpresenter _levelsTileListPresenter;

        // вью
        private readonly LevelsMenuPopupView _view;

        public LevelsMenuPopupPresenter(
            LevelsMenuPopupFactory levelsMenuPopupFactory,            
            LevelsMenuPopupView view)
        {
            _levelsMenuPopupFactory = levelsMenuPopupFactory;             
            _view = view;
        }

        public void Enable()
        {
            _view.SetTitle(TitleName);
            _levelsTileListPresenter = _levelsMenuPopupFactory.CreateLevelTileListPresenter(_view.LevelTileListView);
            _levelsTileListPresenter.Enable();

            _view.CloseRequest += OnCloseRequest;
        }

        public void Disable()
        {
            _levelsTileListPresenter.Disable();
            _view.CloseRequest -= OnCloseRequest;

            UnityEngine.Object.Destroy(_view.gameObject);// удаляем вьюху, по идее призентер не должен заниматься удалением!!!
        }

        private void OnCloseRequest()
        {
            Disable();
        }
    }
}
