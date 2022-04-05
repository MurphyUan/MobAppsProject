using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    [SerializeField] private int hitPoints = 1;

    [SerializeField] private float lifeTime = 10.0f;

    private Rigidbody2D rb;

    public int Damage { get { return hitPoints; } }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;

        Destroy(gameObject, lifeTime);
    }
}
