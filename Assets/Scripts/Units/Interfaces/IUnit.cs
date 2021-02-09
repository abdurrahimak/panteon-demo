using UnityEngine;
using System;
using Panteon.Data;

namespace Panteon.Units
{
    public interface IUnit
    {
        string Name { get; }
        Sprite Sprite { get; }
        Vector2Int CellSize { get; }
        Vector2Int CellPosition { get; }

        event Action<IUnit> Selected;

        UnitTemplate GetTemplate();
        void SetCellPosition(Vector2Int cellPosition);
        void Turn();
    }
}
