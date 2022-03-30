using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioClip crashSound = null;
    [SerializeField] private AudioClip hitSound = null;
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private int healthPoints = 1;

    [SerializeField] private Attack[] attacks;

    private Attack selectedAttack;

    public int ScoreValue { get { return scoreValue;} }

    public int HealthPoints { get { return healthPoints;} }

    public delegate void EnemyKilled(Enemy enemy);

    public static EnemyKilled EnemyKilledEvent;

    public delegate void PlayerKilled();

    public static PlayerKilled PlayerKilledEvent;

    private Transform firePoint;
    
    private void Start() {
        selectedAttack = attacks[Random.Range(0, attacks.Length)];

        firePoint = GetComponentInChildren<Transform>();
    }

    private void Update() {
        // Check if Can Shoot

        // Pick Random Weapon

        // Shoot at Player If Possible
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Enemy is hit by bullet
        var projectile = other.GetComponent<Projectile>();
        if(projectile && other.tag == "playerProj"){
            //play sound
            AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);

            // Decreases Enemy Health and Projectile Health
            healthPoints -= projectile.Damage;

            if(healthPoints > 0)return;

            AudioSource.PlayClipAtPoint(crashSound, Camera.main.transform.position);

            // Spawn Death FX
            Destroy(gameObject);
        }

        if(other.GetComponent<PlayerMovement>()){
            //publish player kill Event
            PublishPlayerKilledEvent();

            Destroy(gameObject);
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
