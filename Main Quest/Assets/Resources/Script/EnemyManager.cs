using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float initialSpawnInterval = 5f;
    public int maxEnemies = 1;

    [Header("Boss Settings")]
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public int totalMobsToDefeatBeforeBoss = 5;

    private int defeatedMobs = 0;
    private bool bossSpawned = false;
    private int currentEnemies = 0;
    private float currentSpawnInterval;

    private bool hasStartedSpawning = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
    }

    public void StartSpawning()
    {
        if (!hasStartedSpawning)
        {
            hasStartedSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2f); // small delay before first spawn

        while (!bossSpawned) // Stop spawning once boss appears
        {
            if (currentEnemies < maxEnemies)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
                currentEnemies++;
            }

            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    public void MobDefeated()
    {
        currentEnemies = Mathf.Max(0, currentEnemies - 1);
        defeatedMobs++;

        if (!bossSpawned && defeatedMobs >= totalMobsToDefeatBeforeBoss)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        if (bossSpawned || bossPrefab == null || bossSpawnPoint == null) return;

        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossSpawned = true;
        Debug.Log("👹 Boss spawned!");
    }

    public void BossDefeated()
    {
        Debug.Log("🏆 Boss defeated! Loading WinScene...");
        SceneManager.LoadScene("Win");
    }
}
