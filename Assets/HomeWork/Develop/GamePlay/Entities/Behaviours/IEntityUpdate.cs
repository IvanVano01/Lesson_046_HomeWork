namespace Assets.HomeWork.Develop.GamePlay.Entities.Behaviours
{
    public interface IEntityUpdate : IEntityBehaviour
    {
       void OnUpdate(float deltaTime);
    }
}
