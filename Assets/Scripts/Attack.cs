using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private float projectileSpeed = 5;

    [SerializeField] private float delay = 1.0f;

    [SerializeField] private AudioClip attackSound;

    public float Delay { get { return delay;} }

    private GameObject projectileParent;

    private AudioController audioController;

    private void Start() {
        projectileParent = GameObject.Find("Projectiles");

        audioController = GameObject.FindObjectOfType<AudioController>();
    }

    public Projectile fireProjectile(Vector2 direction, Transform firePoint){
        Projectile projectile = Instantiate(projectilePrefab, projectileParent.transform);

        projectile.transform.position = firePoint.position;

        audioController.PlayClipFromEffect(attackSound);

        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        return projectile;
    }

    public IEnumerator loopProjectiles(Vector2 direction, Transform firePoint){
        while(true){
            fireProjectile(direction, firePoint);
            yield return new WaitForSeconds(Delay);
        }
    }

    public IEnumerator loopProjectiles(Vector2 direction, Transform[] firePoint){
        int currentFirePointIndex = 0;
        while(true){
            fireProjectile(direction, firePoint[currentFirePointIndex++]);
            if(currentFirePointIndex >= firePoint.Length) currentFirePointIndex = 0;
            yield return new WaitForSeconds(Delay);
        }
    }
}
