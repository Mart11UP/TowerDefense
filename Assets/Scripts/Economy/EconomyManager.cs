using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Economy
{
    public class EconomyManager : MonoBehaviour
    {
        [SerializeField] private int initialMoney = 0;
        public event Action<int, int> OnMoneyAmountChanged;
        private int currentMoney = 0;
        public int CurrentMoney 
        { 
            get { return currentMoney; } 
            private set
            {
                value = Mathf.Max(0, value);
                if (currentMoney != value) OnMoneyAmountChanged?.Invoke(currentMoney, value);
                currentMoney = value;
            }
        }

        private void Start()
        {
            CurrentMoney = initialMoney;
        }

        public void EarnMoney(int amount)
        {
            amount = Mathf.Abs(amount);
            CurrentMoney += amount;
        }

        public void SpendMoney(int amount)
        {
            amount = Mathf.Abs(amount);
            CurrentMoney -= amount;
        }

        public bool CanAfford(int cost)
        {
            return CurrentMoney >= cost;
        }

        public bool AffordabilityChanged(int cost, int previousMoney, int currentMoney, out bool canAffordNow)
        {
            bool couldAffordPreviously = previousMoney >= cost;
            canAffordNow = currentMoney >= cost;
            bool affordabilityChanged = couldAffordPreviously != canAffordNow;

            return affordabilityChanged;
        }
    }
}
