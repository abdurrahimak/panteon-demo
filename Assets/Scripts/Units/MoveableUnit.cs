using UnityEngine;
using System;
using Panteon.Grid;
using Panteon.Extension;
using DG.Tweening;
using System.Collections.Generic;

namespace Panteon.Units
{
    public class MoveableUnit : Unit, IMoveableUnit
    {
        private Vector2Int _targetCellPosition;

        private bool _startMove = false;

        public void Move(Vector2Int cellPosition)
        {
            _startMove = true;
            _targetCellPosition = cellPosition;
            Sequence sequence = DOTween.Sequence();
            List<Vector2Int> path = GridManager.Instance.FindPath(_cellPosition, _targetCellPosition);
            foreach (var cPos in path)
            {
                sequence.Append(transform.DOMove(GridManager.Instance.CellToWorld(cPos.ToVector3Int()), 0.2f));
            }
            sequence.OnComplete(OnMoveSequenceComplete).Play();
            // transform.DOMove(GridManager.Instance.CellToWorld(_targetCellPosition.ToVector3Int()), 1f).Play();
        }

        private void OnMoveSequenceComplete()
        {
            GridManager.Instance.MoveUnit(_cellPosition, _targetCellPosition);
            _cellPosition = _targetCellPosition;
        }

        private void Update()
        {
        }
    }
}
