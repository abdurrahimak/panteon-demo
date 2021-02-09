using UnityEngine;
using System.Collections.Generic;
using Panteon.Units;

namespace Panteon.Grid
{
    public class FindNearestBuildablePosition
    {
        private List<Vector2Int> _neighborOffsets => GridManager.NeighborOffsets;

        private GridManager _gridManager;

        public FindNearestBuildablePosition(GridManager gridManager)
        {
            _gridManager = gridManager;
        }

        public Vector2Int FindNearestBuilableCellPosition(IUnit parentUnit, IUnit createdUnit)
        {
            return FindNearestBuilableCellPosition(parentUnit.CellPosition, parentUnit.CellSize, createdUnit.CellSize);
        }

        public Vector2Int FindNearestBuilableCellPosition(Vector2Int cellPosition, Vector2Int parentCellSize, Vector2Int createdCellSize)
        {
            List<Vector2Int> checkablePositions = new List<Vector2Int>();
            List<Vector2Int> checkedPositions = new List<Vector2Int>();
            for (int i = 0; i < parentCellSize.x; i++)
            {
                for (int j = 0; j < parentCellSize.y; j++)
                {
                    Vector2Int targetCellPos = cellPosition + new Vector2Int(i, j);
                    checkablePositions.Add(targetCellPos);
                }
            }

            return FindCellPositionStraightforward(checkablePositions, checkedPositions, createdCellSize);
        }

        private Vector2Int FindCellPositionStraightforward(List<Vector2Int> checkablePositions, List<Vector2Int> checkedPositions, Vector2Int cellSize)
        {
            List<Vector2Int> nextCellPositions = new List<Vector2Int>();
            foreach (var cellPosition in checkablePositions)
            {
                if (GridManager.Instance.CanBuild(cellPosition, cellSize))
                {
                    return cellPosition;
                }
                else
                {
                    foreach (var neighbor in GetNeighbors(cellPosition))
                    {
                        if (!nextCellPositions.Contains(neighbor) && !checkablePositions.Contains(neighbor) && !checkedPositions.Contains(neighbor))
                        {
                            nextCellPositions.Add(neighbor);
                        }
                    }
                }
            }
            checkedPositions.AddRange(checkablePositions);
            return FindCellPositionStraightforward(nextCellPositions, checkedPositions, cellSize);
        }

        private List<Vector2Int> GetNeighbors(Vector2Int cellPosition)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();
            foreach (var neighborOffset in GridManager.NeighborOffsets)
            {
                neighbors.Add(neighborOffset + cellPosition);
            }
            return neighbors;
        }
    }
}
