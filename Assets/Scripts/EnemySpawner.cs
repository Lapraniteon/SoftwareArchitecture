using System;
using System.Collections;
using SADungeon.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// A enemy spawning system that spawns a prefab either on request or in a coroutine that can be triggered manually or onTriggerEnter.
/// </summary>

public class EnemySpawner : MonoBehaviour
{

    [Header("Spawning")]
    [Tooltip("The enemy prefab to spawn.")]
    [SerializeField]
    private EnemyController enemyPrefabToSpawn;
    [Tooltip("The amount of enemies to spawn per wave.")]
    [SerializeField] 
    private int spawnAmountPerWave;
    [Tooltip("Time in seconds between each wave.")]
    [SerializeField]
    private float timeBetweenWaves;

    private Coroutine _spawnCoroutine;
    [SerializeField] [NaughtyAttributes.ReadOnly]
    private bool _isSpawning;
    
    [Tooltip("Radii inbetween which to spawn")]
    [Header("Spawning Area")]
    [SerializeField]
    private float innerRadius;
    [SerializeField]
    private float outerRadius;

    private bool _spawnEnemies = true;

    [Header("Control")] 
    [Tooltip("Should enemies start spawning automatically on object start?")]
    [SerializeField]
    private bool beginSpawningOnStart = true;

    void Start()
    {
        if (outerRadius == 0)
        {
            _spawnEnemies = false;
            Debug.LogWarning("Outer spawning radius is 0. Enemies will not spawn.");
        }
        else if (innerRadius > outerRadius)
        {
            innerRadius = outerRadius;
            Debug.LogWarning($"Inner spawning radius > outer spawning radius. Inner spawning radius set to outer spawning radius ({outerRadius}).");
        }
        
        if (beginSpawningOnStart)
            StartSpawning();
        
    }
    
    public void StartSpawning()
    {
        _isSpawning = true;
        if (_spawnCoroutine == null)
        {
            _spawnCoroutine = StartCoroutine(EnemySpawnCoroutine());
        }
    }

    public void StopSpawning()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        _spawnCoroutine = null;
        _isSpawning = false;
    }

    private IEnumerator EnemySpawnCoroutine() // Spawn a wave of enemies every [timeBetweenWaves] seconds.
    {
        while (_isSpawning)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            Spawn(spawnAmountPerWave);
        }
    }

    private void Spawn(int amount) // Spawn a specified amount of enemies around the spawner.
    {
        if (!_spawnEnemies)
            return;

        Vector3 origin = transform.position;
        
        for (int i = 0; i < amount; i++)
        {
            Vector2 position = Random.insideUnitCircle * Random.Range(innerRadius, outerRadius); // Randomize spawning position around origin.
            
            EnemyController enemy = Instantiate(enemyPrefabToSpawn, origin + new Vector3(position.x, 0f, position.y), Quaternion.identity);
        }
    }

    private void OnDrawGizmos() // Debug draw inner and outer radii.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, innerRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
