using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HomeWork.Develop.CommonUI
{
    public class IconWithText : MonoBehaviour // View для UI
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;

        public void SetText(string text) => _text.text = text;

        public void SetIcon(Sprite sprite) => _icon.sprite = sprite;        
    }
}
