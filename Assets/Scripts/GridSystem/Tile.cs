using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.GridSystem
{
    public class Tile : MonoBehaviour
    {
        private Vector3Int cellIndex;
        private GridManager gridManager;

        public void InitializeData(Vector3Int cellIndex, GridManager gridManager)
        {
            this.cellIndex = cellIndex;
            this.gridManager = gridManager;
        }

        private void OnDestroy()
        {
            gridManager.ChangeCellState(cellIndex, GridManager.CellState.Free);
        }

        private void OnDisable()
        {
            gridManager.ChangeCellState(cellIndex, GridManager.CellState.Free);
        }
    }
}
