using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public Resource resourcePrefab;  // dont forget to assign mang
    public int minQuantity = 1;
    public int maxQuantity = 5;
    public float[] spawnProbabilities = { 0.5f, 0.3f, 0.1f, 0.05f, 0.05f };
    public Transform Spawn;

    void Start()
    {
        Spawn = gameObject.transform.GetChild(0).transform;
        SpawnResources();
    }

    public void SpawnResources()
    {
        int quantity = GetRandomQuantity(minQuantity, maxQuantity, spawnProbabilities);
        for (int i = 0; i < quantity; i++)
        {
            Instantiate(resourcePrefab, Spawn.position, Quaternion.identity);
        }
    }

    private int GetRandomQuantity(int min, int max, float[] probabilities)
    {
        float rand = Random.value;
        float cumulativeProbability = 0f;

        for (int i = min; i <= max; i++)
        {
            cumulativeProbability += probabilities[i - min];
            if (rand < cumulativeProbability)
            {
                return i;
            }
        }

        return min;
    }
}
