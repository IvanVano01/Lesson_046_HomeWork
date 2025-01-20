﻿using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay.Entities.CommonRegistrators
{
    public class TransformEntityRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _transform;

        public override void Register(Entity entity)
        {
            entity.AddValue(EntityValues.Transform, _transform);
        }
    }
}
