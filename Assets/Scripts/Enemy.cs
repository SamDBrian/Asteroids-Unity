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
    private GameManager GameManager;
    private Rigidbody2D _rigidbody;
    private bool hit = false;

    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GameManager = FindObjectOfType<GameManager>();
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
        // known bug, manuevers can kill momentum. 
    }

    private void Attack(){
        Vector3 firingAngle = Random.insideUnitCircle.normalized; //Random position on edge of spawn radius
        Bullet bullet = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
        bullet.Project(firingAngle);
    }

    private void FixedUpdate(){
       if (hit){
            GameManager.EnemyDestroyed(this);
            Destroy(this.gameObject);
       }
    }

}
