using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float initialSpawnInterval = 5f;
    public int maxEnemies = 1;

    [Header("Difficulty Scaling")]
    public float difficultyIncreaseRate = 0.1f;
    public float minSpawnInterval = 1f;

    private int currentEnemies = 0;
    private float currentSpawnInterval;
    private List<bool> laneActive;
    private float timeSinceLastDifficultyIncrease;
    private bool hasStartedSpawning = false;
    public bool hasTalkedToNPC = false;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        laneActive = new List<bool>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            laneActive.Add(true);
        }
        timeSinceLastDifficultyIncrease = Time.time;
    }

    void Update()
    {
        if (Time.time - timeSinceLastDifficultyIncrease >= 1f)
        {
            timeSinceLastDifficultyIncrease = Time.time;
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - difficultyIncreaseRate);
        }
    }

    public IEnumerator SpawnEnemies()
    {
        if (hasStartedSpawning) yield break;

        yield return new WaitForSeconds(2f); // Optional delay after NPC interaction

        while (true)
        {
            yield return new WaitForSeconds(currentSpawnInterval);

            if (currentEnemies < maxEnemies)
            {
                List<int> activeLanes = new List<int>();
                for (int i = 0; i < laneActive.Count; i++)
                {
                    if (laneActive[i])
                    {
                        activeLanes.Add(i);
                    }
                }

                if (activeLanes.Count > 0)
                {
                    int spawnPointIndex = activeLanes[Random.Range(0, activeLanes.Count)];
                    Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
                    currentEnemies++;
                }
            }
        }
    }
    public IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(SpawnEnemies()); // Start the actual spawning loop here
    }

    public void EnemyDied()
    {
        currentEnemies = Mathf.Max(0, currentEnemies - 1);
    }

    public void StopSpawningFromLane(int index)
    {
        if (index >= 0 && index < laneActive.Count)
        {
            laneActive[index] = false;
            Debug.Log("Stopped spawning from lane: " + index);
        }
    }
}