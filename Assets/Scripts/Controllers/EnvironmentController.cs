using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] private Vector2 scroll = new Vector2(0, 1);

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        moveBackground();
    }

    private void moveBackground(){
        material.mainTextureOffset += scroll * Time.deltaTime;
    }
}
