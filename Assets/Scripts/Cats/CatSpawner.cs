using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    public CatType[] possibleCats;
    public float spawnInterval = 1.5f;
    public float spawnRangeX = 8f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCat), 1f, spawnInterval);
    }
    
    CatType GetWeightedRandomCat()
    {
        float totalWeight = 0f;
        foreach (var gem in possibleCats)
            totalWeight += gem.spawnWeight;

        float randomValue = Random.Range(0, totalWeight);
        float cumulative = 0f;

        foreach (var gem in possibleCats)
        {
            cumulative += gem.spawnWeight;
            if (randomValue <= cumulative)
                return gem;
        }

        return possibleCats[0];
    }

    void SpawnCat()
    {
        Vector3 pos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), transform.position.y, 0);
        CatType selectedCat = GetWeightedRandomCat();
        GameObject catObj = Instantiate(selectedCat.visualPrefab, pos, Quaternion.identity);

        Cat catScript = catObj.GetComponent<Cat>();
        if (catScript != null)
        {
            catScript.catData = selectedCat;
        }
        else
        {
            Debug.LogWarning($"[CatSpawner] Le prefab {selectedCat.visualPrefab.name} n’a pas de script Cat !");
        }
    }
}