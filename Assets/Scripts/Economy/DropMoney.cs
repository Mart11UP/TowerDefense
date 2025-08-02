using UnityEngine;
using Tower.Generic;
using System;

namespace Tower.Economy
{
    public class DropMoney : MonoBehaviour
    {
        [SerializeField] GameObject coinPrefab;
        [Space]
        [Header("Spawn Amount Range")]
        [SerializeField] private int minAmount = 5;
        [SerializeField] private int maxAmount = 10;
        public static event Action<Transform[]> OnMoneyDrop;

        public void DropRandomMoneyAmount()
        {
            int moneyAmount = UnityEngine.Random.Range(minAmount, maxAmount);

            if (!gameObject.TryGetComponent(out RandomSpawner spawner))
                spawner = gameObject.AddComponent<RandomSpawner>();
            spawner.SetSpawnData(coinPrefab);
            Transform[] spawnedCoins = spawner.SpawnAtRandomPosition(moneyAmount);
            gameObject.transform.SetParent(null);
            OnMoneyDrop?.Invoke(spawnedCoins);
            Invoke(nameof(DestroyGameObject), 5);
        }

        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}
