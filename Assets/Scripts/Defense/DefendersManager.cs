using UnityEngine;
using Tower.Data;
using System;
using Tower.GridSystem;

namespace Tower.Defense
{
    public class DefendersManager : MonoBehaviour
    {
        [SerializeField] private DefenderData[] defendersData;
        public static event Action<DefenderData> OnCurrentDefenderChanged;
        private DefenderData currentDefenderData;
        public bool DefenderSelected { get; private set; }
        public DefenderData CurrentDefenderData
        {
            get { return currentDefenderData; }
            set
            {
                if (!value.Equals(currentDefenderData))
                    OnCurrentDefenderChanged?.Invoke(value);
                currentDefenderData = value;
            }
        }
        public static event Action <DefenderData[]> OnDefendersDataUpdated;
        private GridManager gridManager;


        // Start is called before the first frame update
        void Start()
        {
            gridManager = FindAnyObjectByType<GridManager>();
            OnDefendersDataUpdated.Invoke(defendersData);
        }

        public void DefenderSelectionRequest(DefenderData defenderData)
        {
            CurrentDefenderData = defenderData;
            DefenderSelected = true;
        }

        public void PlaceDefenderRequest(Vector2 screenPoint)
        {
            if (!DefenderSelected) return;
            gridManager.InstantiateTile(CurrentDefenderData.prefab, screenPoint);
            DefenderSelected = false;
        }
    }
}
