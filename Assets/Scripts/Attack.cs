using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject projectileParent;

    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private float projectileSpeed = 5;

    [SerializeField] private float waitTime = 1.0f;

    [SerializeField] private AudioClip attackSound;

    [SerializeField] private bool needsLock;

    public float Delay { get { return waitTime;} }

    public Vector2 lockOnTarget(Vector2 other){
        return (other - new Vector2(transform.position.x, transform.position.y)).normalized;
    }

    public void fireProjectile(Vector2 direction, Transform firePoint){
        Projectile projectile = Instantiate(projectilePrefab, projectileParent.transform);

        projectile.transform.position = firePoint.position;

        //Play audioclip (attackSound)

        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
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
