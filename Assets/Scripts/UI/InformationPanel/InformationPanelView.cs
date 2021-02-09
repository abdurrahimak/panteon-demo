using System;
using System.Collections.Generic;
using CoreProject.Resource;
using Panteon.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Panteon.UI
{
    public class InformationPanelView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Text _textName;
        [SerializeField] private Image _image;

        [Header("Production")]
        [SerializeField] private Transform _productionParent;
        [SerializeField] private Transform _productionItemParent;
        [SerializeField] private GameObject _prefabProduction;

        public void Show(bool show)
        {
            _canvas.enabled = show;
        }

        internal void SetInformations(InformationData informationData)
        {
            Show(true);
            _textName.text = informationData.Name;
            _image.sprite = informationData.Sprite;
            PrepareProductions(informationData);
        }

        private void PrepareProductions(InformationData informationData)
        {
            foreach (var item in _productionItemParent.GetComponentsInChildren<Image>())
            {
                GameObject.Destroy(item.gameObject);
            }

            if (informationData.Products != null && informationData.Products.Count > 0)
            {
                _productionParent.gameObject.SetActive(true);
                foreach (InformationData information in informationData.Products)
                {
                    Image image = GameObject.Instantiate(_prefabProduction, _productionItemParent).GetComponent<Image>();
                    image.sprite = information.Sprite;
                    image.gameObject.SetActive(true);
                }
            }
            else
            {
                _productionParent.gameObject.SetActive(false);
            }
        }
    }
}
