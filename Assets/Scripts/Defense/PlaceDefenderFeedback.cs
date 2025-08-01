using System.Collections;
using System.Collections.Generic;
using Tower.GridSystem;
using UnityEngine;

namespace Tower.Defense
{
    public class PlaceDefenderFeedback : MonoBehaviour
    {
        [SerializeField] private GameObject feedbackTile;
        private DefendersManager defendersManager;
        private GridManager gridManager;
        private Vector3 farPosition;

        void Start()
        {
            defendersManager = FindAnyObjectByType<DefendersManager>();
            gridManager = FindAnyObjectByType<GridManager>();
            farPosition = Vector3.one * 1000;
            feedbackTile = Instantiate(feedbackTile, farPosition, Quaternion.identity);
        }

        public void ShowFeedback(Vector2 screenPosition)
        {
            if (!defendersManager.DefenderSelected)
            {
                feedbackTile.transform.position = farPosition;
                return;
            }

            gridManager.PlaceTileInGrid(feedbackTile.transform, screenPosition, false);
        }
    }
}
