using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

[System.Serializable]
public class BaseEnemy : MonoBehaviour
{
    // Variables
    [Header("Base Attributes")]
    [SerializeReference] private Vector2 movementSpeed = new Vector2(10.0f, -1.0f);
    [SerializeReference] private float lifeTime = 10.0f;
    [SerializeReference] private int healthPoints = 1;
    [SerializeReference] private int scoreValue = 1;

    [Header("Restriction")]
    [SerializeReference] private Clamp movementClamp = new Clamp(4.2f,9,5);

    [Header("Audio")]
    [SerializeReference] private AudioClip crashSound;

    // Accessors
    public Vector2 MovementSpeed { get {return movementSpeed;} set { movementSpeed = value;} }
    public float LifeTime { get { return lifeTime;} } 
    public int HealthPoints { get { return healthPoints;} set { healthPoints = value;}}
    public int ScoreValue { get {return scoreValue;} }

    // MovementBounds
    public Clamp MovementClamp { get {return movementClamp;} }

    // Hidden Accessors
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public GameObject explosionFx;
    [HideInInspector] public GameObject explosionParent;
    [HideInInspector] public AudioController audioController;

    // For Movement Bounds
    public class Clamp{
        public float horizontal;
        public float top;
        public float bottom;

        public Clamp(float _horizontal, float _top, float _bottom){
            horizontal = _horizontal;
            top = _top;
            bottom = _bottom;
        }
    }

    private void Awake() {
        // Load Explosion Asset
        explosionFx = Resources.Load<GameObject>("Explosion");

        // TrashCan GameOject
        explosionParent = GameObject.Find("Projectiles");

        // Component Assigners
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        audioController = GameObject.FindObjectOfType<AudioController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Enemy is hit by bullet
        var projectile = other.GetComponent<Projectile>();
        if(projectile){
            // Decrease Enemy Health
            HealthPoints -= projectile.Damage;

            // Destroy Projectile
            Destroy(projectile.gameObject);

            // Enemy no longer has health
            if(healthPoints <= 0) {
                
                // Spawn Explosion at Enemy Position
                GameObject explosion = Instantiate(explosionFx, explosionParent.transform);
                explosion.transform.position = transform.position;

                // Play Audio Clip through AudioMixer
                audioController.PlayClipFromEffect(crashSound);

                // Publish Death Event and Destory GameObject
                Utils.PublishEnemyKilledEvent(this);
                Destroy(gameObject);
            }
            
        }
        //Enemy is hit by player object
        if(other.GetComponent<PlayerMovement>()){
            //publish player kill Event
            Utils.PublishPlayerKilledEvent();
            Debug.Log("Collided with Player");

            // Destroy both Objects
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    public static bool isTurning(Rigidbody2D rb, Transform transform){
        if(rb.velocity.x > 0.1f || rb.velocity.x < -0.1f){
            transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            return true;
        }
        return false;
    }

    // Keeps Enemy within Clamp Bounds, allows Enemy to Enter From Off Screen
    public static void FlipDirection(BaseEnemy enemy){
        if(enemy.transform.position.x >= enemy.MovementClamp.horizontal)
            enemy.rb.velocity = new Vector2(-enemy.MovementSpeed.x, enemy.rb.velocity.y);

        if(enemy.transform.position.x <= -enemy.MovementClamp.horizontal)
            enemy.rb.velocity = new Vector2(enemy.MovementSpeed.x, enemy.rb.velocity.y);

        if(enemy.transform.position.y >= enemy.MovementClamp.top)
            enemy.rb.velocity = new Vector2(enemy.rb.velocity.x, enemy.MovementSpeed.y);

        if(enemy.transform.position.y <= enemy.MovementClamp.bottom)
            enemy.rb.velocity = new Vector2(enemy.rb.velocity.x, -enemy.MovementSpeed.y);
    }

    // Base Spawn Location Algorithm
    public virtual Vector2 GetSpawnLocation(){
        return new Vector2(
            Random.Range(-MovementClamp.horizontal, MovementClamp.horizontal),
            MovementClamp.top + 2
        );
    }
}
