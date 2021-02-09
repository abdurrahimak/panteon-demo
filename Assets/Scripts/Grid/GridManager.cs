using CoreProject.Singleton;
using Panteon.Extension;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using Panteon.Units;

namespace Panteon.Grid
{
    public class GridManager : SingletonComponent<GridManager>
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Tile _tile;

        private Dictionary<Vector2Int, IUnit> _unitOjbects;
        private FindNearestBuildablePosition _findNearestBuildablePosition;
        private PathFinding _pathFinding;

        public Vector3 CellToWorld(Vector3Int cellPosition) => _tilemap.CellToWorld(cellPosition);
        public Vector3Int WorldToCell(Vector3 worldPosition) => _tilemap.WorldToCell(worldPosition);
        public Vector2Int WorldToCell2(Vector2 worldPosition) => _tilemap.WorldToCell2(worldPosition);
        public Vector2Int FindNearestBuilableCellPosition(IUnit parentUnit, IUnit createdUnit) => _findNearestBuildablePosition.FindNearestBuilableCellPosition(parentUnit, createdUnit);
        public List<Vector2Int> FindPath(Vector2Int from, Vector2Int to) => _pathFinding.FindPath(from, to);
        public static List<Vector2Int> NeighborOffsets = new List<Vector2Int>()
        {
            new Vector2Int(-1,-1),
            new Vector2Int(-1,0),
            new Vector2Int(-1,1),
            new Vector2Int(0,1),
            new Vector2Int(1,1),
            new Vector2Int(1,0),
            new Vector2Int(1,-1),
            new Vector2Int(0,-1),
        };

        void Start()
        {
            _pathFinding = new PathFinding(this);
            _findNearestBuildablePosition = new FindNearestBuildablePosition(this);
            _unitOjbects = new Dictionary<Vector2Int, IUnit>();
            _tile.flags = TileFlags.None;
            int mod = 0;
            for (int i = -20; i < 20; i++)
            {
                for (int j = mod - 20; j < 20; j += 2)
                {
                    _tilemap.SetTile(new Vector3Int(i, j, 0), _tile);
                }
                mod += 1;
                mod %= 2;
            }
        }

        void Update()
        {
        }

        public bool CanBuild(Vector2Int cellPosition, Vector2Int cellSize)
        {
            Vector2Int targetPos = Vector2Int.zero;
            for (int i = 0; i < cellSize.x; i++)
            {
                for (int j = 0; j < cellSize.y; j++)
                {
                    targetPos.x = cellPosition.x + i;
                    targetPos.y = cellPosition.y + j;
                    if (_unitOjbects.ContainsKey(targetPos))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CanBuild(IUnit unit)
        {
            return CanBuild(unit.CellPosition, unit.CellSize);
        }

        public void Build(IUnit unit)
        {
            for (int i = 0; i < unit.CellSize.x; i++)
            {
                for (int j = 0; j < unit.CellSize.y; j++)
                {
                    Vector2Int cellPos = unit.CellPosition + new Vector2Int(i, j);
                    _unitOjbects.Add(cellPos, unit);
                }
            }
        }

        public void MoveUnit(Vector2Int from, Vector2Int to)
        {
            IUnit unit = _unitOjbects[from];
            _unitOjbects.Remove(from);
            _unitOjbects.Add(to, unit);
        }
    }
}
