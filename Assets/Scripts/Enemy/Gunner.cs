using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gunner : BaseEnemy
{
    // Variables
    private Coroutine runningTask;

    private Transform[] firePoints;

    private Attack attack;

    void Start()
    {
        firePoints = PlayerWeapons.ConvertFirePoints(GetComponentsInChildren<FirePoint>());

        attack = GetComponentInChildren<Attack>();

        rb.velocity = MovementSpeed;

        runningTask = StartCoroutine(attack.loopProjectiles(Vector2.down, firePoints));
    }

    void Update()
    {
        animator.SetBool("isTurning",isTurning(rb, transform));

        FlipDirection(this);
    }

    public override Vector2 GetSpawnLocation()
    {
        return base.GetSpawnLocation();
    }
}
