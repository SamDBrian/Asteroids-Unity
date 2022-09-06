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
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        GameManager = FindObjectOfType<GameManager>();
    }

    public void SetTrajectory(Vector2 direction){
        //Debug.Log("_rigidbody" + _rigidbody.ToString());
        _rigidbody.AddForce(direction * speed);
        // WHY ARE YOU NULLING?????
        
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate(){
       if (hit){
        GameManager.EnemyDestroyed(this);
        Destroy(this.gameObject);
       }
    }

}
