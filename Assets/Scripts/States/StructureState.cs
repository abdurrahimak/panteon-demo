using System.Collections.Generic;
using CoreProject.States;
using CoreProject.Pool;
using UnityEngine;
using Panteon.Extension;
using Panteon.Grid;
using Panteon.Units;
using Panteon.Data;
using Panteon.Creation;
using Panteon.UI;

namespace Panteon.States
{
    public class StructureState : BaseStrategyGameState
    {
        private IStructure _structure;
        public StructureState(IStructure structure, IGameStateController parentGameStateController, IProductionPanel productionPanel, IInformationPanel informationPanel) : base(parentGameStateController, productionPanel, informationPanel)
        {
            _structure = structure;
        }

        private void InitializeInformation()
        {
            InformationData informationData = UIFactory.Instance.CreateInformationData(_structure);
            _informationPanel.SetInformations(informationData);
        }

        private void InitializeProductionPanel()
        {
            List<UnitTemplate> unitTemplates = (_structure.GetTemplate() as StructureTemplate).Productions;
            if (unitTemplates != null && unitTemplates.Count > 0)
            {
                List<ProductionItem> productionItems = UIFactory.Instance.CreateProductionItems(unitTemplates);
                _productionPanel.SetModels(productionItems);
                _productionPanel.Show(true);
            }
        }

        private void ProductionItem_Selected(ProductionItem productionItem)
        {
            CreateUnit(productionItem);
        }

        private void CreateUnit(ProductionItem productionItem)
        {
            Unit unit = PoolerManager.Instance.GetPoolObject(productionItem.Name, null).GetComponent<Unit>();
            Vector2Int nearestCellPosition = GridManager.Instance.FindNearestBuilableCellPosition(_structure, unit);
            unit.SetCellPosition(nearestCellPosition);
            unit.transform.position = GridManager.Instance.CellToWorld(nearestCellPosition.ToVector3Int());
            GridManager.Instance.Build(unit);
            OnUnitCreated(unit);
        }

        public override void Begin()
        {
            base.Begin();

            _productionPanel.ProductionItemSelected += ProductionItem_Selected;
            InitializeInformation();
            InitializeProductionPanel();
        }

        public override void End()
        {
            base.End();

            _productionPanel.ProductionItemSelected -= ProductionItem_Selected;
            _informationPanel.Show(false);
            _productionPanel.Show(false);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
