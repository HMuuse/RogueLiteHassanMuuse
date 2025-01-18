using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsTospawn;
    [SerializeField]
    private List<GameObject> enemiesToSpawn;
    [SerializeField]
    private List<GameObject> fruitsToSpawn;
    [SerializeField]
    private List<Transform> spawnPoints;

    private float objectDelay = 1f;
    private float enemyDelay = 0.75f;
    private float fruitDelay = 1.25f;
    private int cycleCount = 0;

    private int score;

    private float objectSpawnTimer = 0f;
    private float enemySpawnTimer = 0f;
    private float fruitSpawnTimer = 0f;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    void Start()
    {

    }

    void Update()
    {
        HandleObjectSpawning();
        HandleEnemySpawning();
        HandleFruitSpawning();

        scoreText.text = score.ToString();
    }

    private void HandleObjectSpawning()
    {
        objectSpawnTimer += Time.deltaTime;

        if (objectSpawnTimer >= objectDelay)
        {
            objectSpawnTimer = 0f;

            GameObject currentObjectToSpawn;

            // Every 5 cycles, spawn the object at index 0
            if (cycleCount % 5 == 0)
            {
                currentObjectToSpawn = objectsTospawn[0];
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(1, objectsTospawn.Count);
                currentObjectToSpawn = objectsTospawn[randomIndex];
            }

            // Instantiate the selected object
            Instantiate(currentObjectToSpawn);

            // Increment the cycle count
            cycleCount++;
        }
    }

    private void HandleEnemySpawning()
    {
        enemySpawnTimer += Time.deltaTime;

        if (enemySpawnTimer >= enemyDelay)
        {
            enemySpawnTimer = 0f; // Reset timer

            GameObject currentEnemyToSpawn;

            int randomIndex = UnityEngine.Random.Range(0, enemiesToSpawn.Count);
            currentEnemyToSpawn = enemiesToSpawn[randomIndex];

            // Choose a random spawn point
            int randomSpawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
            Transform currentSpawnPoint = spawnPoints[randomSpawnPointIndex];

            // Check if the spawn point is too close to any existing objects
            if (IsSpawnPointOccupied(currentSpawnPoint.position))
            {
                // If it's occupied, skip this spawn
                return;
            }

            // Instantiate the selected enemy at the chosen spawn point
            Instantiate(currentEnemyToSpawn, currentSpawnPoint.position, currentSpawnPoint.rotation);
        }
    }

    private void HandleFruitSpawning()
    {
        fruitSpawnTimer += Time.deltaTime;

        if (fruitSpawnTimer >= fruitDelay)
        {
            fruitSpawnTimer = 0f; // Reset timer

            GameObject currentFruitToSpawn;

            int randomIndex = UnityEngine.Random.Range(0, fruitsToSpawn.Count);
            currentFruitToSpawn = fruitsToSpawn[randomIndex];

            // Choose a random spawn point
            int randomSpawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
            Transform currentSpawnPoint = spawnPoints[randomSpawnPointIndex];

            // Check if the spawn point is too close to any existing objects
            if (IsSpawnPointOccupied(currentSpawnPoint.position))
            {
                // If it's occupied, skip this spawn
                return;
            }

            // Instantiate the selected fruit at the chosen spawn point
            GameObject fruit = Instantiate(currentFruitToSpawn, currentSpawnPoint.position, currentSpawnPoint.rotation);

            // Subscribe to the OnFruitPickedUp event
            FruitScript fruitScript = fruit.GetComponent<FruitScript>();
            fruitScript.OnFruitPickedUp += FruitScript_OnFruitPickedUp;
        }
    }

    private void FruitScript_OnFruitPickedUp(object sender, int scoreToObtain)
    {
        score += scoreToObtain;
    }

    private bool IsSpawnPointOccupied(Vector2 spawnPosition)
    {
        float radius = 1.0f;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(spawnPosition, radius);

        return hitColliders.Length > 0;
    }
}