using System.Collections.Generic;
using CoreProject.States;
using CoreProject.Pool;
using UnityEngine;
using Panteon.Extension;
using Panteon.Grid;
using Panteon.Units;
using Panteon.Game;
using Panteon.Data;
using Panteon.Creation;
using Panteon.UI;

namespace Panteon.States
{
    public class StructureCreationState : BaseStrategyGameState
    {
        private List<StructureTemplate> _structureTemplates;
        private List<ProductionItem> _productionItems;

        private Structure _selectedStructure = null;
        private AlphaChanger _selectedAlphaChanger = null;

        public StructureCreationState(IGameStateController parentGameStateController, IProductionPanel productionPanel, IInformationPanel informationPanel) : base(parentGameStateController, productionPanel, informationPanel)
        {
        }

        public override void Begin()
        {
            base.Begin();
            CreateAndSendProductionItems();
            _productionPanel.ProductionItemSelected += ProductionPanel_ProductionItemSelected;
        }

        public override void End()
        {
            base.End();
            _productionPanel.ProductionItemSelected -= ProductionPanel_ProductionItemSelected;
            _productionPanel.Show(false);
        }

        private void ProductionPanel_ProductionItemSelected(ProductionItem productionItem)
        {
            CancelBuild();
            _selectedStructure = PoolerManager.Instance.GetPoolObject(productionItem.Name, null).GetComponent<Structure>();
            _selectedAlphaChanger = _selectedStructure.gameObject.GetComponent<AlphaChanger>();
            if (_selectedAlphaChanger == null)
            {
                _selectedAlphaChanger = _selectedStructure.gameObject.AddComponent<AlphaChanger>();
            }
            _selectedAlphaChanger.SetAlpha(0.3f);
            _selectedAlphaChanger.gameObject.SetActive(false);
        }

        private void CancelBuild()
        {
            if (_selectedStructure != null)
            {
                _selectedAlphaChanger.SetAlpha(1f);
                _selectedAlphaChanger.SetColor(Color.white);
                PoolerManager.Instance.SetPoolObjectToPool(_selectedStructure);
                _selectedAlphaChanger = null;
                _selectedStructure = null;
            }
        }

        private void CreateAndSendProductionItems()
        {
            _structureTemplates = StructureFactory.Instance.GetStructerTemplates();
            _productionItems = UIFactory.Instance.CreateProductionItems(_structureTemplates);
            _productionPanel.SetModels(_productionItems);
            _productionPanel.Show(true);
        }

        private void CheckBuildable(bool build, bool turn)
        {
            Vector3 pos = ExtensionMethods.GetMouseWorldPosition();
            Vector3Int cellPosition = GridManager.Instance.WorldToCell(pos);
            _selectedStructure.transform.position = GridManager.Instance.CellToWorld(cellPosition);
            if (GridManager.Instance.CanBuild(cellPosition.ToVector2Int(), _selectedStructure.CellSize))
            {
                _selectedAlphaChanger.SetColor(Color.white);
                if (build)
                {
                    _selectedStructure.SetCellPosition(cellPosition.ToVector2Int());
                    _selectedAlphaChanger.SetAlpha(1f);
                    GridManager.Instance.Build(_selectedStructure);
                    OnUnitCreated(_selectedStructure);
                    _selectedStructure = null;
                    _selectedAlphaChanger = null;
                }
            }
            else
            {
                _selectedAlphaChanger.SetColor(Color.red);
            }

            if (turn)
            {
                _selectedStructure.Turn();
            }
        }

        public override void Update()
        {
            base.Update();

            if (_selectedStructure != null)
            {
                if (!ExtensionMethods.IsPointerOverUIElement())
                {
                    _selectedStructure.gameObject.SetActive(true);
                    CheckBuildable(Input.GetMouseButtonUp(0), Input.GetMouseButtonDown(1));
                }
                else
                {
                    _selectedStructure.gameObject.SetActive(false);
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CancelBuild();
                }
            }
        }
    }
}
