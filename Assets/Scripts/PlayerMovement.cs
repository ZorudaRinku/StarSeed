using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) // UP
        {
            rigidbody2D.position += Vector2.up/20;
        }
        if (Input.GetKey(KeyCode.A)) // LEFT
        {
            rigidbody2D.position += Vector2.left/40;
        }
        if (Input.GetKey(KeyCode.S)) // DOWN
        {
            rigidbody2D.position += Vector2.down/40;
        }
        if (Input.GetKey(KeyCode.D)) // RIGHT
        {
            rigidbody2D.position += Vector2.right/40;
        }
    }
}
