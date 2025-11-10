using SADungeon.Inventory;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    
    public static LootSpawner Instance { get; private set; }

    [SerializeField] 
    private GroundPickup groundPickupPrefab;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnLoot(ItemData[] lootArray, Vector3 center, float range = 1f)
    {
        foreach (ItemData lootData in lootArray)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * range;
            spawnPosition.y = 0f;
            spawnPosition += center;
            
            GroundPickup pickup = Instantiate(groundPickupPrefab, spawnPosition, Quaternion.identity);
            pickup.itemData = lootData;
        }
    }
}
