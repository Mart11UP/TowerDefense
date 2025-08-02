using System.Collections.Generic;
using UnityEngine;

namespace Tower.GridSystem
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [Header("Debug")]
        [Space]
        [SerializeField] private bool showGizmos = true;

        [SerializeField] private Vector3Int cellVisualizationAmount = Vector3Int.one * 2;
        public enum CellState { Free, Occupied }
        private Dictionary<Vector3Int, CellState> cellStates = new();
        private Transform instancesContainer;
        private Transform emptyParent;

        private Grid grid;

        void Start()
        {
            grid = GetComponent<Grid>();
            instancesContainer = new GameObject("GridInstances").transform;
            emptyParent = new GameObject("EmptyParent").transform;
            emptyParent.parent = instancesContainer;
        }

        public bool GetWorldPosition(Vector2 screenPoint, out Vector3 position)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPoint);
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask))
            {
                position = Vector3.one * 1000;
                return false;
            }
            position = hit.point;
            return true;
        }

        public GameObject InstantiateTile(GameObject tilePrefab, Vector2 screenPoint, out bool instantiated)
        {
            GameObject tile = InstantiateTile(tilePrefab, screenPoint);
            if (tile == null)
            {
                instantiated = false;
                return null;
            }

            instantiated = true;
            return tile;
        }

        public GameObject InstantiateTile(GameObject tilePrefab, Vector2 screenPoint)
        {
            if(!GetWorldPosition(screenPoint, out Vector3 worldPosition)) return null;

            return InstantiateTile(tilePrefab, worldPosition);
        }

        public GameObject InstantiateTile(GameObject tilePrefab, Vector3 worldPoint)
        {
            Vector3Int cellIndex = grid.WorldToCell(worldPoint);

            if (!IsCellFree(cellIndex)) return null;

            Transform instance = Instantiate(tilePrefab).transform;

            PlaceTileInGrid(instance, worldPoint, true);
            instance.SetParent(instancesContainer);
            
            Tile tile = instance.gameObject.AddComponent<Tile>();
            tile.InitializeData(cellIndex, this);

            return instance.gameObject;
        }

        public void PlaceTileInGrid(Transform tileTransform, Vector2 screenPoint, bool markCellOccupied)
        {
            GetWorldPosition(screenPoint, out Vector3 worldPosition);
            PlaceTileInGrid(tileTransform, worldPosition, markCellOccupied);
        }

        public void PlaceTileInGrid(Transform tileTransform, Vector3 worldPoint, bool markCellOccupied)
        {
            tileTransform.SetParent(emptyParent);
            Vector3Int cellIndex = grid.WorldToCell(worldPoint);

            if (!IsCellFree(cellIndex)) return;

            if (markCellOccupied) ChangeCellState(cellIndex, CellState.Occupied);

            Vector3 worldCellPosition = grid.GetCellCenterWorld(cellIndex);

            emptyParent.position = worldCellPosition;

            // Normalizes the object's scale so its largest dimension equals 1,
            // allowing uniform scaling to fit inside the cell.
            tileTransform.localScale = NormalizeByGreatestAxis(tileTransform.localScale);

            // Scales the tile uniformly based on the cell's smallest side to ensure it fits proportionally.
            float smallestSide = Mathf.Min(grid.cellSize.x, Mathf.Min(grid.cellSize.y, grid.cellSize.z));
            emptyParent.localScale = Vector3.one * smallestSide;

            // Positions the tile at the bottom of the cell, aligned with the floor.
            Vector3 floorOffset = ((grid.cellSize.y / smallestSide) * 0.5f - tileTransform.localScale.y * 0.5f) * Vector3.down;
            tileTransform.localPosition = floorOffset;
        }

        private bool IsCellFree(Vector3Int cellIndex)
        {
            return !cellStates.TryGetValue(cellIndex, out var state) || state == CellState.Free;
        }

        public void ChangeCellState(Vector3Int cellIndex, CellState cellState)
        {
            cellStates[cellIndex] = cellState;
        }

        public static Vector3 NormalizeByGreatestAxis(Vector3 vector)
        {
            float maxAxis = Mathf.Max(vector.x, Mathf.Max(vector.y, vector.z));
            return maxAxis == 0 ? Vector3.zero : vector / maxAxis;
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            grid = gameObject.GetComponent<Grid>();
            for (int x = -cellVisualizationAmount.x / 2; x < Mathf.CeilToInt(cellVisualizationAmount.x / 2.0f); x++)
                for (int y = -cellVisualizationAmount.y / 2; y < Mathf.CeilToInt(cellVisualizationAmount.y / 2.0f); y++)
                    for (int z = -cellVisualizationAmount.z / 2; z < Mathf.CeilToInt(cellVisualizationAmount.z / 2.0f); z++)
                        Gizmos.DrawWireCube(grid.GetCellCenterWorld(new(x, y, z)), grid.cellSize);
        }

        private void OnDestroy()
        {
            if (instancesContainer != null) Destroy(instancesContainer.gameObject);
        }
    }
}
