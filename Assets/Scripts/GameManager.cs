using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    [SerializeReference]
    private GameOverScreen gameOverScreen;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public int score = 0;
    [SerializeReference]
    private int asteroidsAlive = 0;
    [SerializeReference]
    private int levelsCleared = 0;
    
    // private void Update(){
    //     if (asteroidsAlive == 0) {
    //         IncrementLevelsCleared();
    //     }
    // }

    public void AsteroidDestroyed(Asteroid asteroid){       
        this.explosion.transform.position = asteroid.transform.position;
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

    public void MissileDestroyed(Missile missile){
        this.explosion.transform.position = missile.transform.position;
        this.explosion.Play();
    }

    public void PlayerDied(){
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.lives--;

        if (this.lives <= 0){
            GameOver();
        }else{
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }
    
    private void Respawn(){
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
    }

    private void GameOver(){
        gameOverScreen.DisplayGameOver();
    }

    public void LevelCleared(){
        levelsCleared++;
        Respawn();
    }
    public int GetLevelsCleared(){
        return levelsCleared;
    }

    public void IncrementAsteroidsAlive(){
        Debug.Log("I ran");
        asteroidsAlive++;
    }

    public int GetAsteroidsAlive(){
        return asteroidsAlive;
    }

}
