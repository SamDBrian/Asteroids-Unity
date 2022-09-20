using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float size = 1.0f;
    public float speed = 20.0f;
    public float firstShotDelay = 2f;
    public float fireRate = 2f;
    private GameManager gameManager;
    private Rigidbody2D _rigidbody;
    private AudioManager audioManager;
    private bool hit = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start() {
        InvokeRepeating(nameof(Attack), firstShotDelay, fireRate);
        InvokeRepeating(nameof(Manuever), 3f, 10f);
    }

    public void SetTrajectory(Vector2 direction){
        _rigidbody.AddForce(direction * speed);
    }   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet"){
            hit = true;
        }
    }

    private void Manuever(){
        Vector3 newCourse = Random.insideUnitCircle.normalized;
        _rigidbody.AddForce(newCourse * speed);
        // known feature, manuevers can kill momentum. 
    }

    private void Attack(){
        Vector3 firingAngle = Random.insideUnitCircle.normalized; //Random position on edge of spawn radius
        Bullet bullet = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
        bullet.Project(firingAngle);
    }

    private void FixedUpdate(){
       if (hit){
            gameManager.EnemyDestroyed(this);
            Destroy(this.gameObject);
       }
    }

}
