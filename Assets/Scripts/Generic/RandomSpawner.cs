using System.Collections;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;

namespace Tower.Generic
{
    public class RandomSpawner : MonoBehaviour
    {
        [SerializeField] private int spawnAmount = 1;
        [Space]
        [Header("Wait Time Range")]
        [SerializeField] private float minWaitTime = 5;
        [SerializeField] private float maxWaitTime = 10;
        [SerializeField] private bool playOnAwake = false;
        private IEnumerator spawnerRoutine;
        private Transform instancesContainer;

        [System.Serializable]
        public struct SpawnObjectData
        {
            public GameObject prefab;
            [Range(1, 1000)]
            public float weight;
            public SpawnObjectData(GameObject prefab, float weight)
            {
                this.prefab = prefab; 
                this.weight = weight;
            }
        }

        [SerializeField] private SpawnObjectData[] spawnObjectData;
        private float[] weights;

        // Start is called before the first frame update
        void Awake()
        {
            if (playOnAwake)
            {
                UpdateWeights();
                spawnerRoutine = SpawnAtRandomIntervals();
                StartCoroutine(SpawnAtRandomIntervals());
            }
            instancesContainer = new GameObject("Instances " + name).transform;
        }

        public void UpdateWeights()
        {
            weights = new float[spawnObjectData.Length];
            for (int i = 0; i < spawnObjectData.Length; i++)
                weights[i] = spawnObjectData[i].weight;
        }

        public void SetSpawnData(SpawnObjectData[] spawnData)
        {
            spawnObjectData = spawnData;
            UpdateWeights();
        }

        public void SetSpawnData(SpawnObjectData spawnData)
        {
            SpawnObjectData[] spawnObjects = new SpawnObjectData[1];
            spawnObjects[0] = spawnData;
            SetSpawnData(spawnObjects);
        }

        public void SetSpawnData(GameObject prefab)
        {
            SetSpawnData(new SpawnObjectData(prefab, 1));
        }

        public void StartSpawner()
        {
            StartCoroutine(spawnerRoutine);
        }

        public void StopSpawner()
        {
            StopCoroutine(spawnerRoutine);
        }

        private IEnumerator SpawnAtRandomIntervals()
        {
            while (true)
            {
                float waitTime = Random.Range(minWaitTime, maxWaitTime);
                yield return new WaitForSeconds(waitTime);

                SpawnAtRandomPosition(spawnAmount);
            }
        }
        public void SpawnAtRandomPosition(int spawnAmount)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                float randomX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
                float randomY = Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2);
                float randomZ = Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2);
                Vector3 randomOffset = new(randomX, randomY, randomZ);
                Vector3 position = transform.position + randomOffset;

                int index = GetIndexByWeight(weights);
                GameObject prefab = spawnObjectData[index].prefab;

                GameObject instance = Instantiate(prefab, position, transform.rotation);
                instance.transform.SetParent(instancesContainer);
            }
        }

        int GetIndexByWeight(float[] weights)
        {
            float roulette = 0;
            foreach (float weight in weights)
                roulette += weight;

            float randomRate = Random.Range(0f, 1f) * roulette;
            float currentPossitionInRoulette = 0;

            for (int index = 0; index < weights.Length; index++)
            {
                currentPossitionInRoulette += weights[index];
                if (currentPossitionInRoulette >= randomRate)
                    return index;
            }
            return -1;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }

        private void OnDestroy()
        {
            if (instancesContainer != null) Destroy(instancesContainer.gameObject);
        }
    }
}
