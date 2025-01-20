namespace Assets.HomeWork.Develop.CommonServices.SceneManagment
{
    public interface IOutputSceneArgs
    {
        IInputSceneArgs NextInputSceneArgs { get; } // выходными параметрами для перехода в другую сцену, будут входные параметры для сцену куда мы хотим перейти
    }

    public abstract class OutputSceneArgs : IOutputSceneArgs// абстрактный клас выходных аргументов сцены
    {
        protected OutputSceneArgs(IInputSceneArgs nextInputSceneArgs)// конструктор
        {
            NextInputSceneArgs = nextInputSceneArgs;
        }

        public IInputSceneArgs NextInputSceneArgs { get; }
    }

    public class OutputGameplayArgs : OutputSceneArgs //выходные аргументы из сцены "Gameplay"
    {
        public OutputGameplayArgs(IInputSceneArgs nextInputSceneArgs) : base(nextInputSceneArgs)
        {
            // можно передать результат игры из сцены "Gameplay"
        }
    }

    public class OutputMainMenuArgs : OutputSceneArgs //выходные аргументы из сцены "MainMenu"
    {
        public OutputMainMenuArgs(IInputSceneArgs nextInputSceneArgs) : base(nextInputSceneArgs)
        {
            // можно передать какие-нить параметры из главного меню
        }
    }

    public class OutputBootstrapArgs : OutputSceneArgs //выходные аргументы из сцены "Bootstrap"
    {
        public OutputBootstrapArgs(IInputSceneArgs nextInputSceneArgs) : base(nextInputSceneArgs)
        {
            // можно передать какие-нить параметры из "Bootstrap"
        }
    }
}
