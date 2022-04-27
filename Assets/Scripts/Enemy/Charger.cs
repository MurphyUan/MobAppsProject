using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Charger : BaseEnemy
{
    private GameObject target;

    private Coroutine runningTask;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2();

        target = GameObject.Find("Player(Clone)");
        runningTask = StartCoroutine(collideWithTarget(gameObject, target, rb, this.MovementSpeed));

        Destroy(gameObject, this.LifeTime);
    }

    void Update(){
        animator.SetBool("isTurning",isTurning(rb, transform));
    }

    public override Vector2 GetSpawnLocation()
    {
        return base.GetSpawnLocation();
    }

    public static IEnumerator collideWithTarget(GameObject parent, GameObject target, Rigidbody2D rb, Vector2 movementSpeed){
        while(true){
            if(parent == null)break;
            Vector2 dir = Utils.LockOnTarget(parent.transform.position, target.transform.position);
            rb.velocity = new Vector2(
                dir.x * movementSpeed.x,
                movementSpeed.y
            );
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
