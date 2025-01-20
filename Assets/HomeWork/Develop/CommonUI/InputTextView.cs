using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HomeWork.Develop.CommonUI
{
    public class InputTextView : MonoBehaviour
    {
        public event Action<string> InputedSymbols;

        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _button;

        public string GetInputText() => _inputField.text;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnInputSymbols);
        }

        private void OnInputSymbols()
        {
            InputedSymbols?.Invoke(_inputField.text);
        }
    }    
}
