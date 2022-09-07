using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float size = 1.0f;
    public float speed = 20.0f;
    private GameManager GameManager;
    private Rigidbody2D _rigidbody;
    private bool hit = false;

    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GameManager = FindObjectOfType<GameManager>();
    }

    public void SetTrajectory(Vector2 direction){
        _rigidbody.AddForce(direction * speed);
    }   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet"){
            hit = true;
        }
    }
    
    private void FixedUpdate(){
       if (hit){
        GameManager.EnemyDestroyed(this);
        Destroy(this.gameObject);
       }
    }

}
