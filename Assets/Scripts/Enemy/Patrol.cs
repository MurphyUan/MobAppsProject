using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BaseEnemy
{
    // Variables
    private Transform[] firePoints;

    private Attack attack;

    private GameObject target;

    private void Start() {
        // Get Location of Firepoint
        firePoints = PlayerWeapons.ConvertFirePoints(GetComponentsInChildren<FirePoint>());

        // Get Attack From Components
        attack = GetComponentInChildren<Attack>();

        // Update Movement
        rb.velocity = MovementSpeed;

        // Assign target
        target = GameObject.Find("Player(Clone)");

        // Start Attack
        StartCoroutine(ShootAtTarget(attack.Delay));
    }

    private void Update() {
        // Update Animator
        animator.SetBool("isTurning",isTurning(rb, transform));

        // Update Orientation
        FlipDirection(this);
    }

    // Returns a Random Location with Reference to Movement Clamp
    public override Vector2 GetSpawnLocation(){
        return new Vector2(
            (Mathf.Sign(Random.Range(-1,1)) * MovementClamp.horizontal) + 2,
            Random.Range(MovementClamp.top, MovementClamp.bottom)
        );
    }

    // Self Explanatory, Coroutine with Delay that Fires a Projectile at a target
    private IEnumerator ShootAtTarget(float delay){
        while(true){
            Projectile projectile = attack.fireProjectile(Utils.LockOnTarget(this.transform.position, target.transform.position), firePoints[0]);
            projectile.transform.eulerAngles = new Vector3(0, 0, Utils.SlopeBetweenTwoPoints(-target.transform.position));
            yield return new WaitForSeconds(delay);
        }
    }
}
