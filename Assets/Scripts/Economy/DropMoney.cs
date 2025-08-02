using UnityEngine;
using Tower.Generic;

namespace Tower.Economy
{
    public class DropMoney : MonoBehaviour
    {
        [SerializeField] GameObject coinPrefab;
        [Space]
        [Header("Spawn Amount Range")]
        [SerializeField] private int minAmount = 5;
        [SerializeField] private int maxAmount = 10;
        private EconomyManager economyManager;

        private void Start()
        {
            economyManager = FindAnyObjectByType<EconomyManager>();
        }

        public void DropRandomMoneyAmount()
        {
            int moneyAmount = Random.Range(minAmount, maxAmount);

            if (!gameObject.TryGetComponent(out RandomSpawner spawner))
                spawner = gameObject.AddComponent<RandomSpawner>();
            spawner.SetSpawnData(coinPrefab);
            spawner.SpawnAtRandomPosition(moneyAmount);
            economyManager.EarnMoney(moneyAmount);
            gameObject.transform.SetParent(null);
        }
    }
}
