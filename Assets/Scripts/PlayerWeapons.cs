using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;

    [SerializeField] private Attack[] attacks;

    [SerializeField] private Animator animator;

    private AudioSource audioSource;

    private Attack selectedAttack;

    private Coroutine attackCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        selectedAttack = attacks[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Fire Projectiles
        if(Input.GetButtonDown("Fire1")){
            
            if(attackCoroutine != null)StopCoroutine(attackCoroutine);
            animator.SetBool("isShooting", true);
            attackCoroutine = StartCoroutine(selectedAttack.loopProjectiles(Vector2.up, firePoints));
        }

        if(Input.GetButtonUp("Fire1")){
            if(attackCoroutine == null)return;

            animator.SetBool("isShooting", false);
            StopCoroutine(attackCoroutine);
        }

        // Clear Bullets off screen, check if meter is full
        if(Input.GetKeyDown(KeyCode.Space)){
            // if ultimateCharge > 10

            // Spawn attack[1] at player location
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            
        }
    }
}
