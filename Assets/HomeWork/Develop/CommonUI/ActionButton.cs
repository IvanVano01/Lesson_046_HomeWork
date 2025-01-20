using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HomeWork.Develop.CommonUI
{
    public class ActionButton : MonoBehaviour // просто для удобства тестирования
    {
        [SerializeField] private Button _button;

        public void Initialize(Action action) => _button.onClick.AddListener(() => action?.Invoke());
    }
}