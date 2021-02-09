using CoreProject.States;
using UnityEngine;
using Panteon.Extension;
using Panteon.Grid;
using Panteon.Units;
using Panteon.Creation;
using Panteon.UI;
using Panteon.Data;

namespace Panteon.States
{
    public class MovableUnitState : BaseStrategyGameState
    {
        private IMoveableUnit _movableUnit;
        public MovableUnitState(IMoveableUnit movalbeUnit, IGameStateController parentGameStateController, IProductionPanel productionPanel, IInformationPanel informationPanel) : base(parentGameStateController, productionPanel, informationPanel)
        {
            _movableUnit = movalbeUnit;
        }

        private void InitializeInformation()
        {
            InformationData informationData = UIFactory.Instance.CreateInformationData(_movableUnit);
            _informationPanel.SetInformations(informationData);
        }


        public override void Begin()
        {
            base.Begin();

            InitializeInformation();
        }

        public override void End()
        {
            base.End();

            _informationPanel.Show(false);
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetMouseButtonDown(1))
            {
                Vector3 pos = ExtensionMethods.GetMouseWorldPosition();
                Vector3Int cellPosition = GridManager.Instance.WorldToCell(pos);
                if (GridManager.Instance.CanBuild(cellPosition.ToVector2Int(), Vector2Int.one))
                {
                    _movableUnit.Move(cellPosition.ToVector2Int());
                }
                else
                {
                    Debug.Log("Cannot move the area!");
                }
            }
        }
    }
}
