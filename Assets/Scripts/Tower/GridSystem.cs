using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Towers
{
    public class GridSystem : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Vector3Int cellVisualizationAmount = Vector3Int.one * 2;

        private Grid grid;
        // Start is called before the first frame update
        void Start()
        {
            grid = GetComponent<Grid>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Vector2 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask)) return;

            Vector3Int cellIndex = grid.WorldToCell(hit.point);
            print(cellIndex);
            Vector3 worldCellPosition = grid.GetCellCenterWorld(cellIndex);

            GameObject instance = Instantiate(tilePrefab, worldCellPosition, Quaternion.identity);

            instance.transform.parent = transform;
        }

        private void OnDrawGizmos()
        {
            grid = gameObject.GetComponent<Grid>();
            for (int x = Mathf.FloorToInt(-cellVisualizationAmount.x / 2.0f); x < cellVisualizationAmount.x / 2; x++)
                for (int y = Mathf.FloorToInt(-cellVisualizationAmount.y / 2.0f); y < cellVisualizationAmount.y / 2; y++)
                    for (int z = Mathf.FloorToInt(-cellVisualizationAmount.z / 2.0f); z < cellVisualizationAmount.z / 2; z++)
                        Gizmos.DrawWireCube(grid.GetCellCenterWorld(new(x, y, z)), grid.cellSize);
        }
    }
}
