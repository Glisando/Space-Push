using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance = null;

    [SerializeField] Transform parentObject;
    List<GameObject> smallEnemies;
    List<GameObject> mediumEnemies;
    EnemySpawnerTimer spawnCooldown;

    float minSpawnCooldown = 0.3f;
    float maxSpawnCooldown = 1f;

    float mediumEnemySpawnChance = 0.3f;
    float smallEnemySpawnChance = 0.7f;

    float offset = 2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadEnemies();

        spawnCooldown = gameObject.AddComponent<EnemySpawnerTimer>();
        spawnCooldown.Duration = minSpawnCooldown;

        EnemySpawnerTimer.EnemySpawnCooldownFinished += SpawnEnemy;
        spawnCooldown.Run();
    }

    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector3 enemyPos = new Vector3(ScreenDimensions.ScreenRight + offset, 0, 0);
        enemyPos.y = Random.Range(ScreenDimensions.ScreenBottom, ScreenDimensions.ScreenTop);

        GameObject enemyToSpawn;

        if (Random.value <= smallEnemySpawnChance)
            enemyToSpawn = smallEnemies[Random.Range(0, smallEnemies.Count - 1)];
        else
            enemyToSpawn = mediumEnemies[Random.Range(0, mediumEnemies.Count - 1)];

        GameObject tmp = Instantiate(enemyToSpawn, enemyPos, Quaternion.identity);

        tmp.transform.parent = parentObject;

        spawnCooldown.Run();
    }

    void LoadEnemies()
    {
        smallEnemies = new List<GameObject>();

        smallEnemies.Add(Resources.Load<GameObject>("Enemies/SmallEnemy1"));
        smallEnemies.Add(Resources.Load<GameObject>("Enemies/SmallEnemy2"));
        smallEnemies.Add(Resources.Load<GameObject>("Enemies/SmallEnemy3"));

        mediumEnemies = new List<GameObject>();

        mediumEnemies.Add(Resources.Load<GameObject>("Enemies/MediumEnemy1"));
        mediumEnemies.Add(Resources.Load<GameObject>("Enemies/MediumEnemy2"));
        mediumEnemies.Add(Resources.Load<GameObject>("Enemies/MediumEnemy3"));
    }

    private void OnDisable()
    {
        EnemySpawnerTimer.EnemySpawnCooldownFinished -= SpawnEnemy;
    }
}
