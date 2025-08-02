using UnityEngine;
using Tower.Data;
using System;
using Tower.GridSystem;
using Tower.Economy;

namespace Tower.Defense
{
    public class DefendersManager : MonoBehaviour
    {
        [SerializeField] private DefenderData[] defendersData;
        public static event Action<DefenderData> OnCurrentDefenderChanged;
        public static event Action OnDefenderSelectionRejected;
        public static event Action<DefenderData, bool> OnCanAffordChanged;
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
        private EconomyManager economyManager;


        // Start is called before the first frame update
        void Start()
        {
            gridManager = FindAnyObjectByType<GridManager>();
            economyManager = FindAnyObjectByType<EconomyManager>();
            OnDefendersDataUpdated.Invoke(defendersData);
            economyManager.OnMoneyAmountChanged += UpdateAffordability;
            UpdateAffordability(100000, economyManager.CurrentMoney);
        }

        private void OnDisable()
        {
            economyManager.OnMoneyAmountChanged -= UpdateAffordability;
        }

        public void DefenderSelectionRequest(DefenderData defenderData)
        {
            CurrentDefenderData = defenderData;
            if (!economyManager.CanAfford(defenderData.Cost))
            {
                OnDefenderSelectionRejected?.Invoke();
                return;
            }
            DefenderSelected = true;
        }

        public void PlaceDefenderRequest(Vector2 screenPoint)
        {
            if (!DefenderSelected) return;
            if (!economyManager.CanAfford(currentDefenderData.Cost)) return;

            gridManager.InstantiateTile(CurrentDefenderData.prefab, screenPoint, out var instantiated);
            DefenderSelected = false;

            if (!instantiated) return;

            economyManager.SpendMoney(currentDefenderData.Cost);
        }

        private void UpdateAffordability(int previousMoney, int currentMoney)
        {
            foreach (var defenderData in defendersData)
            {
                bool affordabilityChanged = economyManager.AffordabilityChanged(defenderData.Cost, previousMoney, currentMoney, out var canAffordNow);
                if (!affordabilityChanged) continue;

                OnCanAffordChanged?.Invoke(defenderData, canAffordNow);
            }
        }
    }
}
