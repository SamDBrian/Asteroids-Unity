using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Missile missilePrefab;
    public float thrustSpeed = 1;
    public float turnSpeed = 225;
    public float railGunThrust = 5;
    public bool enableRailGunThrust = true;
    public int PDCBurstLength = 5;

    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;
    public int maxMissiles = 1;
    private int missilesActive = 0;
    private bool launchMissileRight = true;
    private float rotZ;

    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable(){
        this.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions (Player)");
        this.Invoke(nameof(TurnOnCollision), 2.0f);
    }

    private void TurnOnCollision() {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void Update()
    {
        _thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            _turnDirection = 1.0f;
            rotZ += Time.deltaTime * _turnDirection * turnSpeed;
        }else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turnDirection = -1.0f;
            rotZ += Time.deltaTime * _turnDirection * turnSpeed;
        }else{
            _turnDirection = 0.0f;
        }

        

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            transform.RotateAround (transform.position, transform.forward, 180f);
        }
        
        if (Input.GetKeyDown(KeyCode.Space)){
            FireRailgun();
        }

        if (Input.GetMouseButtonUp(0) && (missilesActive < maxMissiles)){
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FireMissile(target);
        }

    }

    private void FixedUpdate(){
        if (_thrusting) {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
        if (_turnDirection != 0.0f){
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        

    }

    private void FireRailgun(){
        Debug.Log("Firing Railgun!");
        Bullet bullet = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
        if (enableRailGunThrust) {
            _rigidbody.AddForce(-this.transform.up * (this.railGunThrust));
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision){
    //     if (collision.gameObject.tag == "Asteroid"){
    //         FirePDC(collision);
    //     }
    // }

    private void FireMissile(Vector3 target) {
        Debug.Log("Missile away! Target: " + target);
        missilesActive++;
        Missile missile = Instantiate(this.missilePrefab, transform.position, transform.rotation);
        Vector3 finalTrajectory = target;
        missile.Launch(finalTrajectory, this, launchMissileRight);
        launchMissileRight = !launchMissileRight;
    }

    public void recordMissileDestroyed(){
        missilesActive--;
    }

    private void FirePDC(Collider2D collision){       
        // PDC Currently fires extremely fast
        // PDC only fires accurately at targets in the lower and left half of the screen.
        // Pretty sure the problem has something to do with how I'm projecting the shot.
        // WAIT!!! The for loop needs a delay between shots!!!
        for (int i = 0; i <= PDCBurstLength; i++) {
            Bullet bullet = Instantiate(this.bulletPrefab, transform.position, transform.rotation);
            //Vector3 target =  new Vector3(collision.gameObject.)
            bullet.Project(collision.gameObject.transform.position);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Asteroid"){
            _rigidbody.velocity = Vector3.zero; //zero out player movement
            _rigidbody.angularVelocity = 0.0f; 

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }

    }

}
