using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float DeployDistance = .5f;
    public float initialSpeed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float maxLifetime = 10.0f;
    public float speed = 5.0f;
    public ParticleSystem explosion;
    public Bullet bulletPrefab;
    private Vector2 target;
    private Rigidbody2D _rigidbody;
    private Player player;
    private Vector3 startingPoint;
    private Quaternion _lookRotation;
    private Vector2 _direction;
    private AudioManager _audioManager;
    private bool targetLocked = false;
    private bool launchMissileRight;

    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioManager = FindObjectOfType<AudioManager>();
        Debug.Log("audio manager: " + _audioManager);
    }

    public void Launch(Vector3 direction, Player player, bool launchMissileRight){
        // set necessary targeting and reference information.
        target = direction;
        this.player = player; 
        startingPoint = transform.position;
        this.launchMissileRight = launchMissileRight;

        // deploy missile to the right or left of the ship
        if (launchMissileRight) {
            _rigidbody.AddForce(transform.right * initialSpeed);
        } else { 
            _rigidbody.AddForce(-transform.right * initialSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Missiles die if hit before they reach their destination
        FindObjectOfType<GameManager>().MissileDestroyed(this);
        _audioManager.Play("missileFailed");
        player.recordMissileDestroyed();
        Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        
        float distance = Vector3.Distance(transform.position, startingPoint);

        if (distance > 0.5f) {
             TargetLocked();  
        }

        if (targetLocked) {
            //turn to face target
            Vector2 lookDir = target - _rigidbody.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.rotation = angle;
            //end
            transform.position = Vector3.MoveTowards(transform.position, target, (speed * Time.deltaTime));
            Vector2 pos = transform.position;
            if (pos == target) {
                
                _audioManager.Play("burst");
                Detonate();
            }        
        }
    }

    private void TargetLocked(){
        targetLocked = true;
    }

    private void Detonate(){          
            //north
            Bullet bulletN = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
            bulletN.Project(new Vector2(0.0f, 1.0f));
            //North-East
            Bullet bulletNE = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
            bulletNE.Project(new Vector2(0.75f, 0.75f));
            //east
            Bullet bulletE = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
            bulletE.Project(new Vector2(1.0f, 0.0f));
            //South-East
            Bullet bulletSE = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
            bulletSE.Project(new Vector2(0.75f, -0.75f));
            //south
            Bullet bulletS = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
            bulletS.Project(new Vector2(0.0f, -1.0f));
            //south-west
            Bullet bulletSW = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
            bulletSW.Project(new Vector2(-0.75f, -0.75f));
            //west
            Bullet bulletW = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
            bulletW.Project(new Vector2(-1.0f, 0.0f));
            //north-west
            Bullet bulletNW = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
            bulletNW.Project(new Vector2(-0.75f, 0.75f));

            player.recordMissileDestroyed();
            Destroy(this.gameObject);
    }

    
}
