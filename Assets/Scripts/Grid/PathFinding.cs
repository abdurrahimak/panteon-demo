using UnityEngine;
using System.Collections.Generic;

namespace Panteon.Grid
{
    public class PathFinding
    {
        private class PathNode
        {
            public Vector2Int CellPosition;

            public int gCost;
            public int hCost;
            public int fCost;

            public PathNode cameFromNode;
            internal bool IsWalkable;

            public PathNode(Vector2Int cellPos, bool isWalkable)
            {
                CellPosition = cellPos;
                IsWalkable = isWalkable;
                gCost = 99999999;
                CalculateFCost();
                cameFromNode = null;
            }

            public void CalculateFCost()
            {
                fCost = gCost + hCost;
            }

            public override string ToString()
            {
                return CellPosition.ToString();
            }
        }

        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private GridManager _gridManager;
        private List<PathNode> _leafPathNodes;
        private List<PathNode> _usedNodeList;
        private Dictionary<Vector2Int, PathNode> _pathNodesByCellPos;
        private int maxIteration = 2000;

        public PathFinding(GridManager gridManager)
        {
            _pathNodesByCellPos = new Dictionary<Vector2Int, PathNode>();
            _leafPathNodes = new List<PathNode>();
            _usedNodeList = new List<PathNode>();
            _gridManager = gridManager;
        }

        public List<Vector2Int> FindPath(Vector2Int from, Vector2Int to)
        {
            _pathNodesByCellPos.Clear();
            _leafPathNodes.Clear();
            _usedNodeList.Clear();

            if (!_gridManager.CanBuild(to, Vector2Int.one))
            {
                // Invalid Path
                return null;
            }
            PathNode startNode = new PathNode(from, true);
            _pathNodesByCellPos.Add(from, startNode);
            PathNode endNode = new PathNode(to, true);
            _pathNodesByCellPos.Add(to, endNode);

            _leafPathNodes = new List<PathNode>() { startNode };
            _usedNodeList = new List<PathNode>();

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            while (_leafPathNodes.Count > 0)
            {
                PathNode currentNode = GetLowestFCostNode(_leafPathNodes);
                if (currentNode == endNode)
                {
                    return CalculatePath(currentNode);
                }

                _leafPathNodes.Remove(currentNode);
                _usedNodeList.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
                {
                    if (_usedNodeList.Contains(neighbourNode)) continue;
                    if (!neighbourNode.IsWalkable)
                    {
                        _usedNodeList.Add(neighbourNode);
                        continue;
                    }

                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!_leafPathNodes.Contains(neighbourNode))
                        {
                            _leafPathNodes.Add(neighbourNode);
                        }
                    }
                }
            }
            return null;
        }

        private List<Vector2Int> GetNeighbors(Vector2Int cellPosition)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();
            foreach (var neighborOffset in GridManager.NeighborOffsets)
            {
                var targetPos = neighborOffset + cellPosition;
                if (_gridManager.CanBuild(targetPos, Vector2Int.one))
                {
                    neighbors.Add(targetPos);
                }
            }
            return neighbors;
        }

        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            List<PathNode> neighbourList = new List<PathNode>();

            foreach (var cellPosition in GetNeighbors(currentNode.CellPosition))
            {
                if (_pathNodesByCellPos.ContainsKey(cellPosition))
                {
                    neighbourList.Add(_pathNodesByCellPos[cellPosition]);
                }
                else
                {
                    PathNode pathNode = new PathNode(cellPosition, _gridManager.CanBuild(cellPosition, Vector2Int.one));
                    _pathNodesByCellPos.Add(cellPosition, pathNode);
                    neighbourList.Add(pathNode);
                }
            }

            return neighbourList;
        }

        private List<Vector2Int> CalculatePath(PathNode endNode)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            path.Add(endNode.CellPosition);
            PathNode currentNode = endNode;
            Debug.Log(currentNode.CellPosition);
            while (currentNode.cameFromNode != null)
            {
                path.Add(currentNode.cameFromNode.CellPosition);
                currentNode = currentNode.cameFromNode;
            }
            path.Reverse();
            return path;
        }

        private int CalculateDistanceCost(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.CellPosition.x - b.CellPosition.x);
            int yDistance = Mathf.Abs(a.CellPosition.y - b.CellPosition.y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
        {
            PathNode lowestFCostNode = pathNodeList[0];
            for (int i = 1; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                {
                    lowestFCostNode = pathNodeList[i];
                }
            }
            return lowestFCostNode;
        }
    }
}
