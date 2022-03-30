using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float left = -4.0f;

    [SerializeField] private float right = 6.0f;
    [SerializeField] private float ceilingHeight = 10.0f;
    [SerializeField] private float floorHeight = 10.0f;

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

    private void MovePlayer(){
        // update velocity vector
        rb.velocity = new Vector2(
            Input.GetAxis("Horizontal") * playerSpeed,
            Input.GetAxis("Vertical") * playerSpeed);

        // update position vector, keep player on screen
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, left, right), 
            Mathf.Clamp(rb.position.y, floorHeight, ceilingHeight));
    }

    private void MovePlayer(float x, float y){
        rb.position = new Vector2(
            Mathf.Clamp(x, left, right),
            Mathf.Clamp(y, floorHeight, ceilingHeight));
    }
}
