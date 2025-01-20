using TMPro;
using UnityEngine;

namespace Assets.HomeWork.Develop.CommonUI
{
    public class OnlyText : MonoBehaviour // вьюха для отображения текста
    {
        [SerializeField] private TMP_Text _textValue;
        [SerializeField] private TMP_Text _textNameValue;

        public void SetTextValue(string text) => _textValue.text = text;
        public void SetTextName(string text) => _textNameValue.text = text;
    }
}
