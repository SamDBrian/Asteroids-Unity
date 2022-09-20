using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Missile missilePrefab;
    [SerializeReference]
    private AudioManager audioManager;
    public float thrustSpeed = 1;
    public float turnSpeed = 225;
    public float railGunThrust = 5;
    public bool enableRailGunThrust = true;
    public int PDCBurstLength = 5;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _thrusting;
    private float _turnDirection;
    private SpriteRenderer _spriteRenderer;
    public int maxMissiles = 1;
    private int missilesActive = 0;
    private bool launchMissileRight = true;
    private float rotZ;
    private bool startAudioLoop = true;
    private bool invincible = false;
    private bool alive;
    
    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable(){
        alive = true;
        this.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions (Player)");
        invincible = true;
        _spriteRenderer.color = new Color(.5f,.5f,.5f,1f);
        this.Invoke(nameof(TurnOnCollision), 2.0f);
    }

    private void TurnOnCollision() {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        _spriteRenderer.color = new Color(1,1,1,1);
        invincible = false;
    }


    void Update()
    {
        _thrusting = (Input.GetKey(KeyCode.W));
        _animator.SetBool("Thrusting", _thrusting);

        if (Input.GetKey(KeyCode.A)){
            _turnDirection = 1.0f;
            rotZ += Time.deltaTime * _turnDirection * turnSpeed;
        }else if (Input.GetKey(KeyCode.D)){
            _turnDirection = -1.0f;
            rotZ += Time.deltaTime * _turnDirection * turnSpeed;
        }else{
            _turnDirection = 0.0f;
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
            if (startAudioLoop){
                audioManager.Play("engine");
                startAudioLoop = false;
            }
        } else {
            audioManager.Stop("engine");
            startAudioLoop = true;
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
    public void resetMissilesActive(){
        missilesActive = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if ((collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy") && alive){
            alive = false;
            KillMomentum(); 
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }

    public void KillMomentum(){
        _rigidbody.velocity = Vector3.zero; //zero out player movement
        _rigidbody.angularVelocity = 0.0f; 
    }

    public void ResetRotation(){
        transform.rotation = new Quaternion(0,0,0,0);
        rotZ = 0f;
    }

}
