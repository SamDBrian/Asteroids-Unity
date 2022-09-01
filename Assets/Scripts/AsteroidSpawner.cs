using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public bool EnableAsteroidSpawning = true;
    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;
    public float trajectoryVariance = 15.0f;
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        if (EnableAsteroidSpawning) {
            InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn() {
        for (int i = 0; i < this.spawnAmount; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance; //Random position on edge of spawn radius
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
