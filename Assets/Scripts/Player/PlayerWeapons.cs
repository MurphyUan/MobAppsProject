using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private Attack[] attacks;

    [SerializeField] private Animator animator;

    private Transform[] firePoints;

    private AudioSource audioSource;

    private Attack selectedAttack;

    private Coroutine attackCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        firePoints = ConvertFirePoints(GetComponentsInChildren<FirePoint>());

        audioSource = GetComponent<AudioSource>();

        selectedAttack = attacks[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Fire Projectiles
        if(Input.GetButtonDown("Fire1")){
            
            // Fixes multiple coroutines running at the same time
            if(attackCoroutine != null)StopCoroutine(attackCoroutine);

            // Update Animator
            animator.SetBool("isShooting", true);
            attackCoroutine = StartCoroutine(selectedAttack.loopProjectiles(Vector2.up, firePoints));
        }

        //Stop Firing Projectiles
        if(Input.GetButtonUp("Fire1")){
            if(attackCoroutine == null)return;

            //Update Animator
            animator.SetBool("isShooting", false);
            StopCoroutine(attackCoroutine);
        }
    }

    // Converts Objects of Type FirePoint to transform from an array
    public static Transform[] ConvertFirePoints(FirePoint[] array){
        Transform[] newArray = new Transform[array.Length];

        for(int i = 0; i < array.Length; i++){
            newArray[i] = array[i].transform;
        }

        return newArray;
    }

    // Stop Firing on Destroy
    private void OnDestroy() {
        StopAllCoroutines();
    }
}
