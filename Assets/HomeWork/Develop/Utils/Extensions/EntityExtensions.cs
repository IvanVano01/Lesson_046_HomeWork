using Assets.HomeWork.Develop.GamePlay.Entities;
using Assets.HomeWork.Develop.Utils.Reactive;

namespace Assets.HomeWork.Develop.Utils.Extensions
{
    public static class EntityExtensions// класс экстеншенов
    {
        public static bool TryTakeDamage(this Entity entity, float damage)
        {

            if (entity.TryGetTakeDamageRequest(out ReactiveEvent<float> damageRequest))
            {
                damageRequest?.Invoke(damage);
                return true;
            }

            return false;
        }
    }
}
