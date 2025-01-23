using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private List<GameObject> objectsTospawn;
    [SerializeField]
    private List<GameObject> enemiesToSpawn;
    [SerializeField]
    private List<GameObject> fruitsToSpawn;
    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private List<GameObject> spawnedObjects;

    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private TextMeshProUGUI gameOverText;

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

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        PlayerController.Instance.OnPlayerDeath += PlayerController_OnPlayerDeath;
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
            GameObject spawnedObject = Instantiate(currentObjectToSpawn);

            spawnedObjects.Add(spawnedObject);

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
            GameObject spawnedEnemy = Instantiate(currentEnemyToSpawn, currentSpawnPoint.position, currentSpawnPoint.rotation);

            spawnedObjects.Add(spawnedEnemy);
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

            spawnedObjects.Add(fruit);

            // Subscribe to the OnFruitPickedUp event
            FruitScript fruitScript = fruit.GetComponent<FruitScript>();
            fruitScript.OnFruitPickedUp += FruitScript_OnFruitPickedUp;
        }
    }

    public void RemoveSpawnedObject(GameObject gameObject)
    {
        if (spawnedObjects.Contains(gameObject))
        {
            spawnedObjects.Remove(gameObject);
        }
    }

    private void FruitScript_OnFruitPickedUp(object sender, FruitScript.FruitPickedUpEventArgs args)
    {
        // Update the score
        score += args.Score;
        
        switch (args.Type)
        {
            case FruitScript.FruitType.Apple:

                PlayerController.Instance.EnableShield();
                break;
            case FruitScript.FruitType.Cherry:

                foreach (GameObject obj in spawnedObjects)
                {
                    Destroy(obj);
                }
                break;
            case FruitScript.FruitType.Banana:

                PlayerController.Instance.SlowDownTime(0.5f, 5f);
                break;
        }
    }

    private void PlayerController_OnPlayerDeath(object sender, EventArgs e)
    {
        Time.timeScale = 0;
        // Show the game over screen
        gameOverScreen.gameObject.SetActive(true);
        gameOverText.text = $"Game Over!\nYour Score: {score}";
    }
    
    private bool IsSpawnPointOccupied(Vector2 spawnPosition)
    {
        float radius = 1.0f;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(spawnPosition, radius);

        return hitColliders.Length > 0;
    }

    public int GetScore()
    {
        return score;
    }
}