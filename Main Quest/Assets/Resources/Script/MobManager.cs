using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public static MobManager Instance;

    private bool bossSpawned = false;
    public int totalMobs = 0;
    private int defeatedMobs = 0;

    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterMob()
    {
        totalMobs++;
    }

    public void MobDefeated()
    {
        defeatedMobs++;
        if (defeatedMobs >= totalMobs)
        {
            SpawnBoss();
        }
    }

    public void SpawnBoss()
    {
        if (bossSpawned || bossPrefab == null || bossSpawnPoint == null) return;

        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossSpawned = true;
        Debug.Log("👹 Boss spawned by EnemySpawner!");
    }
}
