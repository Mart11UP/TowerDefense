using DG.Tweening;
using System.Collections;
using System.Numerics;
using UnityEngine;

namespace Tower.Economy
{
    public class MoneyDropCollector : MonoBehaviour
    {
        [Header("Money Travel Duration Range")]
        [SerializeField] private float maxDuration = 1;
        [SerializeField] private float minDuration = 0.5f;

        [Header("Next Coin Interval Range")]
        [Range(0, 1)] [SerializeField] private float maxTime = 0.5f;
        [Range(0, 1)] [SerializeField] private float minTime = 0.01f;
        private EconomyManager economyManager;

        private void AnimateMoneyDrop(Transform[] moneyDrop)
        {
            StartCoroutine(MoneyDropAnimation(moneyDrop));
        }

        private IEnumerator MoneyDropAnimation(Transform[] moneyDrop)
        {
            foreach (Transform coin in moneyDrop)
            {
                float randomDuration = Random.Range(minDuration, maxDuration);
                coin.DOMove(transform.position, randomDuration).SetEase(Ease.InQuad);
                int rotationLoops = 3;
                coin.DORotate(new UnityEngine.Vector3(0, 360, 0), maxDuration / rotationLoops, RotateMode.Fast).SetLoops(rotationLoops).SetEase(Ease.Linear);
                Invoke(nameof(EarnCoin), randomDuration);

                float randomWait = Random.Range(minTime, maxTime);

                yield return new WaitForSeconds(randomWait);
            }
            yield return new WaitForSeconds(maxDuration);

            foreach (Transform coin in moneyDrop) Destroy(coin.gameObject);
        }

        private void EarnCoin()
        {
            economyManager.EarnMoney(1);
        }

        private void OnEnable()
        {
            economyManager = FindAnyObjectByType<EconomyManager>();
            DropMoney.OnMoneyDrop += AnimateMoneyDrop;
        }

        private void OnDisable()
        {
            DropMoney.OnMoneyDrop -= AnimateMoneyDrop;
        }
    }
}
