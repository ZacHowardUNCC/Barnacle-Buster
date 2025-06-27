using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarnacleSpawnerEndless : MonoBehaviour, BarnacleSpawner
{

    public GameObject basicBarnaclePrefab;
    public GameObject smallBarnacleClusterPrefab;
    public float spawnInterval = 2f;
    public int totalBarnacles = 20;

    private List<Transform> spawnPoints = new List<Transform>();
    private List<Transform> availableSpots = new List<Transform>();

    private bool isSpawning = false;
    private int currentBarnacleCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }

        // Make a copy for availability tracking
        availableSpots = new List<Transform>(spawnPoints);

        // Start spawning over time
        StartCoroutine(SpawnBarnacles());
    }

    IEnumerator SpawnBarnacles()
    {
        isSpawning = true;

        while (currentBarnacleCount < totalBarnacles && availableSpots.Count > 0)
        {
            SpawnOneBarnacle();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    void SpawnOneBarnacle()
    {
        if (availableSpots.Count == 0) return;

        // Pick a random available spot
        int index = Random.Range(0, availableSpots.Count);
        Transform spot = availableSpots[index];

        // Spawn and parent it for cleanliness
        GameObject barnacleToSpawn = Random.value < 0.5f ? basicBarnaclePrefab : smallBarnacleClusterPrefab; // choosing between the 2 barnacles based on rand num for bool. "?" is a ternary operator for conditional short hand.
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f)); // Rotate the prefabs for variety
        GameObject barnacle = Instantiate(barnacleToSpawn, spot.position, randomRotation); // creates copy of prefab in game world
        barnacle.transform.parent = spot; // makes spawn spot the parent for easy viewing in hierarchy

        // Track usage
        availableSpots.RemoveAt(index);
        currentBarnacleCount++;

        // Optional: let the barnacle know where it is so it can notify spawner when destroyed
        // Try BasicBarnacle first
        var basic = barnacle.GetComponent<BasicBarnacle>();
        if (basic != null)
        {
            basic.spawner = this;
            basic.occupiedSpot = spot;
        }
        else
        {
            // Then try SmallBarnacleCluster
            var cluster = barnacle.GetComponent<SmallBarnacleCluster>();
            if (cluster != null)
            {
                cluster.spawner = this;
                cluster.occupiedSpot = spot;
            }
        }
    }

    // Call this from the barnacle when it's destroyed
    public void FreeUpSpot(Transform spot)
    {
        availableSpots.Add(spot);
        currentBarnacleCount--;

        // Optionally: start spawning again if under cap
        if (currentBarnacleCount < totalBarnacles && !isSpawning)
        {
            StartCoroutine(SpawnBarnacles());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
