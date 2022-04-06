using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound = null;
    [SerializeField] private Attack attack;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private int healthPoints = 1;

    public int ScoreValue { get { return scoreValue;} set { scoreValue = value; } }
    public int HealthPoints { get { return healthPoints;} set { healthPoints = value; } }

    public delegate void EnemyKilled(Enemy enemy);
    public static EnemyKilled EnemyKilledEvent;

    public delegate void PlayerKilled();
    public static PlayerKilled PlayerKilledEvent;

    private Transform firePoint;
    private GameObject player;
    private Rigidbody2D rb;
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.down * movementSpeed;

        player = GameObject.Find("Player");

        firePoint = GetComponentInChildren<Transform>();

        if(attack == null)StartCoroutine(collideWithPlayer());
        else InvokeRepeating("useAttack",attack.Delay, attack.Delay);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Enemy is hit by bullet
        var projectile = other.GetComponent<Projectile>();
        if(projectile){
            Debug.Log("Hit Player Projectile");

            //AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);

            // Decrease Enemy Health
            HealthPoints -= projectile.Damage;

            Destroy(other.gameObject);

            if(healthPoints <= 0) {

                PublishEnemyKilledEvent();

                // Spawn Death FX
                Destroy(gameObject);
            }
        }

        if(other.GetComponent<PlayerMovement>()){
            //publish player kill Event
            PublishPlayerKilledEvent();

            Destroy(gameObject);
        }
    }

    private void useAttack(){
        attack.fireProjectile(Vector2.down, firePoint);
    }

    private IEnumerator collideWithPlayer(){
        while(true){
            Vector2 dir = attack.lockOnTarget(new Vector2(player.transform.position.x,player.transform.position.y));
            rb.velocity = new Vector2(
                dir.x * movementSpeed,
                dir.y * movementSpeed
            );
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void PublishEnemyKilledEvent(){
        if(EnemyKilledEvent != null)
                EnemyKilledEvent(this);
    }

    private void PublishPlayerKilledEvent(){
        if(PlayerKilledEvent != null)
                PlayerKilledEvent();
    }
}
