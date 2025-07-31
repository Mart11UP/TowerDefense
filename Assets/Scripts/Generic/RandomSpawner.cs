using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("Wait Time Range")]
    [SerializeField] private float minWaitTime = 5;
    [SerializeField] private float maxWaitTime = 10;
    private IEnumerator spawnerRoutine;

    [System.Serializable]
    private struct SpawnObjectData
    {
        public GameObject prefab;
        [Range(1, 1000)]
        public float weight;
    }

    [SerializeField] private SpawnObjectData[] spawnObjects;
    private float[] weights;

    // Start is called before the first frame update
    void Start()
    {
        weights = new float[spawnObjects.Length];
        for (int i = 0; i < spawnObjects.Length; i++) 
            weights[i] = spawnObjects[i].weight;

        spawnerRoutine = SpawnAtRandomIntervals();
        StartCoroutine(SpawnAtRandomIntervals());
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

            SpawnAtRandomPosition();
        }
    }
    private void SpawnAtRandomPosition()
    {
        float randomX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
        float randomY = Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2);
        float randomZ = Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2);
        Vector3 randomOffset = new(randomX, randomY, randomZ);
        Vector3 position = transform.position + randomOffset;

        int index = GetIndexByWeight(weights);
        GameObject prefab = spawnObjects[index].prefab;

        Instantiate(prefab, position, transform.rotation);
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
}
