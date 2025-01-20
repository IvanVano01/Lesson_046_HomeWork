using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HomeWork.Develop.CommonUI
{
    public class ButtonWithText : MonoBehaviour
    {
        public event Action PressedButton;

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        public void SetText(string text) => _text.text = text;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickButton);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton()
        {
            PressedButton?.Invoke();
        }
    }
}
