using System.Collections.Generic;
using CoreProject.States;
using CoreProject.Pool;
using UnityEngine;
using Panteon.Units;
using Panteon.Game;
using Panteon.Creation;
using Panteon.UI;

namespace Panteon.States
{
    public class GameController : MonoBehaviour, IGameStateController
    {
        private ProductionPanel _productionPanel;
        private InformationPanel _informationPanel;
        private CameraController _cameraController;

        private IGameState _currentState;
        private List<IUnit> _createdUnits;

        void Start()
        {
            PoolerManager.Instance.Initialize();
            _informationPanel = new InformationPanel();
            _productionPanel = new ProductionPanel();
            _cameraController = new CameraController();
            _createdUnits = new List<IUnit>();

            _cameraController.SwitchStructureCreationState += CameraController_SwitchStructureCreationState;
            var state = StateFactory.Instance.CreateStructureCreationState(this, _productionPanel, _informationPanel);
            SwitchGameState(state);
        }

        private void CameraController_SwitchStructureCreationState()
        {
            if (!(_currentState is StructureCreationState))
            {
                var state = StateFactory.Instance.CreateStructureCreationState(this, _productionPanel, _informationPanel);
                SwitchGameState(state);
            }
        }

        void Update()
        {
            _cameraController.Update();
            _currentState?.Update();
        }

        public void SwitchGameState(IGameState gameState)
        {
            _currentState?.End();
            UnregisterGameStateEvents(_currentState);
            _currentState = gameState;
            RegisterGameStateEvents(_currentState);
            _currentState?.Begin();
        }

        private void UnregisterGameStateEvents(IGameState currentState)
        {
            if (currentState is BaseStrategyGameState baseGameState)
            {
                baseGameState.UnitCreated -= Unit_Created;
            }
        }

        private void RegisterGameStateEvents(IGameState currentState)
        {
            if (currentState is BaseStrategyGameState baseGameState)
            {
                baseGameState.UnitCreated += Unit_Created;
            }
        }

        private void Unit_Created(IUnit unit)
        {
            _createdUnits.Add(unit);
            unit.Selected += Unit_Selected;
        }

        private void Unit_Destroyed(IUnit unit)
        {
            _createdUnits.Remove(unit);
            unit.Selected -= Unit_Selected;
        }

        private void Unit_Selected(IUnit unit)
        {
            if (unit is IStructure structure)
            {
                var state = StateFactory.Instance.CreateStructureState(structure, this, _productionPanel, _informationPanel);
                SwitchGameState(state);
            }
            else if (unit is IMoveableUnit moveableUnit)
            {
                var state = StateFactory.Instance.CreateMovableUnitState(moveableUnit, this, _productionPanel, _informationPanel);
                SwitchGameState(state);
            }
        }
    }
}
