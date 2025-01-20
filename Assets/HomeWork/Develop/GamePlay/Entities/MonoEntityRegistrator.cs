using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.Entities
{
    public abstract class MonoEntityRegistrator : MonoBehaviour// регистратор сущностей монобехов
    {
        public abstract void Register(Entity entity);
    }
}
