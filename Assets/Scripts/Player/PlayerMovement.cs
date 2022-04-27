using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    // Variables
    [SerializeField] private float playerSpeed = 10.0f;
    
    [SerializeField] private float left = -4.0f;

    [SerializeField] private float right = 6.0f;
    [SerializeField] private float ceilingHeight = 10.0f;
    [SerializeField] private float floorHeight = 10.0f;

    [SerializeField] private Animator PlayerBody;
    [SerializeField] private Animator PlayerGun;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Obstacle"){
            Utils.PublishPlayerKilledEvent();
            Destroy(gameObject);
        }
    }

    private void MovePlayer(){
        // update velocity vector
        rb.velocity = new Vector2(
            Input.GetAxis("Horizontal") * playerSpeed,
            Input.GetAxis("Vertical") * playerSpeed);

        // update position vector, keep player on screen
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, left, right), 
            Mathf.Clamp(rb.position.y, floorHeight, ceilingHeight));

        // Flip Character based on Orientation
        ProcessDirection();
    }

    private void ProcessDirection(){
        bool isMoving = false;
        // if player isnt moving in the horizontal axis
        if(rb.velocity.x != 0){
            transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            isMoving = true;
        }

        // Update Animators
        PlayerGun.SetBool("isTurning", isMoving);
        PlayerBody.SetBool("isTurning", isMoving);
    }
}
