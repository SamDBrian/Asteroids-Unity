using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    [SerializeReference]
    private GameOverScreen gameOverScreen;
    [SerializeReference]
    private StartScreen startMenu;
    [SerializeReference]
    private ScoreManager scoreUI;
    [SerializeReference]
    private ControlsScreen controlScreen;
    [SerializeReference]
    private AsteroidSpawner AsteroidSpawner;
    public Boundary boundary;
    public AudioManager audioManager;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public int score = 0;
    [SerializeReference]
    private int asteroidsAlive = 0;
    [SerializeReference]
    private int enemiesAlive = 0;
    [SerializeReference]
    private int levelsCleared = 0;
    private AudioSource _audioSource;

    void Awake(){
        this.player.gameObject.SetActive(false);
        startMenu.DisplayStartScreen();
        _audioSource = GetComponent<AudioSource>();
    }

    public void AsteroidDestroyed(Asteroid asteroid){       
        this.explosion.transform.position = asteroid.transform.position;
        audioManager.Play("objectDestroyed");
        this.explosion.Play();

        if (asteroid.size < .75f) {
            score += 100;
        } else if (asteroid.size < 1.2f){
            score += 50;
        } else {
            score += 25;
        }

        asteroidsAlive--;
    }

    public void EnemyDestroyed(Enemy enemy){
        this.explosion.transform.position = enemy.transform.position;
        this.explosion.Play();
        audioManager.Play("objectDestroyed");
        score += 100;
        enemiesAlive--;
        
    }

    public void MissileDestroyed(Missile missile){
        this.explosion.transform.position = missile.transform.position;
        this.explosion.Play();
    }

    public void PlayerDied(){
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        audioManager.Play("playerDied");
        this.lives--;

        if (this.lives <= 0){
            GameOver();
        }else{
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }
    
    public void Respawn(){
        this.player.transform.position = Vector3.zero;
        this.player.KillMomentum();
        this.player.ResetRotation();
        this.player.gameObject.SetActive(true);
    }

    private void GameOver(){
        gameOverScreen.DisplayGameOver();
    }

    public void LevelCleared(){
        levelsCleared++;
        this.player.gameObject.SetActive(false);
        KillOrphans();
        Invoke(nameof(Respawn), .5f);
    }

    private void KillOrphans(){
        Missile[] orphanMissiles = FindObjectsOfType<Missile>();
        for (int i = 0; i < orphanMissiles.Length; i++ ){
            Destroy(orphanMissiles[i].gameObject);
        }
        Bullet[] orphanBullets = FindObjectsOfType<Bullet>();
        for (int i = 0; i < orphanBullets.Length; i++ ){
            Destroy(orphanBullets[i].gameObject);
        }
        player.resetMissilesActive();
    }

    public int GetLevelsCleared(){
        return levelsCleared;
    }

    public void IncrementAsteroidsAlive(){
        asteroidsAlive++;
    }

    public int GetAsteroidsAlive(){
        return asteroidsAlive;
    }
    public void IncrementEnemiesAlive(){
        enemiesAlive++;
    }

    public int GetEnemiesAlive(){
        return enemiesAlive;
    }

    public void DisplayStartScreen(){
        startMenu.DisplayStartScreen();
    }

    public void DisplayControlsScreen(){
        controlScreen.DisplayControlsScreen();
    }

    public void DisplayUI(){
        scoreUI.DisplayScoreScreen();
    }
    public void ActivateEnemySpawning(){
        AsteroidSpawner.EnableEnemySpawning();
    }

}
