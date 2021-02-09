using System.Collections.Generic;
using CoreProject.Resource;
using CoreProject.Singleton;
using Panteon.Data;
using Panteon.UI;
using Panteon.Units;
using UnityEngine;

namespace Panteon.Creation
{
    public class UIFactory : SingletonClass<UIFactory>
    {
        public InformationData CreateInformationData(IUnit unit)
        {
            InformationData information = new InformationData();
            information.Name = unit.Name;
            information.Sprite = unit.Sprite;
            return information;
        }

        public InformationData CreateInformationData(IStructure structure)
        {
            InformationData information = new InformationData();
            information.Name = structure.Name;
            information.Sprite = structure.Sprite;
            information.Products = new List<InformationData>();
            foreach (var production in (structure.GetTemplate() as StructureTemplate).Productions)
            {
                InformationData data = new InformationData()
                {
                    Name = production.UnitName,
                    Sprite = production.Sprite,
                };
                information.Products.Add(data);
            }
            return information;
        }

        public List<ProductionItem> CreateProductionItems(List<StructureTemplate> structureTemplates)
        {
            List<ProductionItem> productionItems = new List<ProductionItem>();
            foreach (var structure in structureTemplates)
            {
                ProductionItem productionItem = new ProductionItem()
                {
                    Name = structure.UnitName,
                    Sprite = structure.Sprite,
                    Object = structure,
                };
                productionItems.Add(productionItem);
            }
            return productionItems;
        }

        internal ProductionPanelView CreateProductionPanelView(Transform parent = null)
        {
            return GameObject.Instantiate(ResourceManager.Instance.GetResource<GameObject>("ProductionPanelView"), parent).GetComponent<ProductionPanelView>();
        }

        internal InformationPanelView CreateInformationPanelView(Transform parent = null)
        {
            return GameObject.Instantiate(ResourceManager.Instance.GetResource<GameObject>("InformationPanelView"), parent).GetComponent<InformationPanelView>();
        }

        public List<ProductionItem> CreateProductionItems(List<UnitTemplate> unitTemplate)
        {
            List<ProductionItem> productionItems = new List<ProductionItem>();
            foreach (var unit in unitTemplate)
            {
                ProductionItem productionItem = new ProductionItem()
                {
                    Name = unit.UnitName,
                    Sprite = unit.Sprite,
                    Object = unit,
                };
                productionItems.Add(productionItem);
            }
            return productionItems;
        }
    }
}