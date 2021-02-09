using System;
using UnityEngine;
using UnityEngine.UI;

namespace Panteon.UI
{
    public class ProductionItemView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;

        public event Action ItemClicked;

        private void Start()
        {
            _button.onClick.AddListener(ProductionItem_Clicked);
        }

        private void ProductionItem_Clicked()
        {
            ItemClicked?.Invoke();
        }

        internal void UpdateView(Sprite sprite, string name)
        {
            _image.sprite = sprite;
            _text.text = name;
        }
    }
}