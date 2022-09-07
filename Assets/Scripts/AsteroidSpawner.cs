using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public Enemy enemyPrefab;
    public int baseSpawnAmount = 4;
    public float spawnDistance = 15.0f;
    public float trajectoryVariance = 15.0f;
    public float initialEnemySpawnDelay = 5f;
    public float enemySpawnDelay = 20f;
    public bool unlimitedModeEnabled = false;
    public float unlimitedModeSpawnRate = 3.0f;
    public float unlimitedModeSpawnDistance = 10.0f;
    private int spawnDirectionModifier = 1; // 1 for outward, -1 for inward
    private int spawnAmount;
    [SerializeField]
    private Boundary boundary;
    [SerializeField]
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        if (unlimitedModeEnabled){
            spawnAmount = 1;
            InvokeRepeating(nameof(Spawn), this.unlimitedModeSpawnRate, this.unlimitedModeSpawnRate);
            spawnDistance = unlimitedModeSpawnDistance;
            spawnDirectionModifier = -1;
        } else {
            spawnAmount = baseSpawnAmount;
            Spawn();
        }
        InvokeRepeating(nameof(SpawnEnemy), initialEnemySpawnDelay, enemySpawnDelay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!unlimitedModeEnabled && gameManager.GetAsteroidsAlive() == 0 && gameManager.GetEnemiesAlive() == 0){
            gameManager.LevelCleared();
            spawnAmount = baseSpawnAmount + gameManager.GetLevelsCleared();
            Spawn();
        }
    }

    private void Spawn() {
        for (int i = 0; i < spawnAmount; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance; //Random position on edge of spawn radius
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = 1.5f;
            asteroid.SetTrajectory(rotation * spawnDirection * spawnDirectionModifier);
            gameManager.IncrementAsteroidsAlive();
        }
    }

    private void SpawnEnemy(){
        float spawnX = 0f;
        Vector2 spawnDirection = new Vector2();
        if (Random.value < 0.5f){
            //left
            spawnX = (-boundary.transform.localScale.x / 2) - 1f; 
            spawnDirection = new Vector2(1.0f, 0.0f);
        }else{
            //right
            spawnX = (boundary.transform.localScale.x / 2) + 1f; 
            spawnDirection = new Vector2(-1.0f, 0.0f);
        }
        float spawnYRange = (boundary.transform.localScale.y / 2) - 1f;
        float spawnY = Random.Range(-spawnYRange, spawnYRange);

        Vector3 spawnPoint = new Vector3(spawnX, spawnY);

        Enemy enemy = Instantiate(this.enemyPrefab, spawnPoint, this.transform.rotation);
        enemy.size = .5f;
        enemy.SetTrajectory(spawnDirection);
        gameManager.IncrementEnemiesAlive();
    }
}
