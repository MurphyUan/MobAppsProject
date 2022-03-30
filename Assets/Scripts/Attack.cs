using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private GameObject projectileParent;

    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private float projectileSpeed;

    [SerializeField] private float waitTime = 1.0f;

    [SerializeField] private AudioClip attackSound;

    [SerializeField] private bool needsLock;

    public float Delay { get { return waitTime * Time.deltaTime;} }

    private void Awake() {
        projectileParent = GameObject.Find("Projectiles");
    }

    public Vector2 lockOnTarget(Vector2 other){
        return (other - new Vector2(transform.position.x, transform.position.y)).normalized;
    }

    public void fireProjectile(Vector2 direction, Transform firePoint){
        Projectile projectile = Instantiate(projectilePrefab, projectileParent.transform);

        projectile.transform.position = firePoint.position;

        //Play audioclip (attackSound)

        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }
}
