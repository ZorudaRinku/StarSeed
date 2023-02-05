using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator armsAnimator, bodyAnimator, legsAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        armsAnimator = transform.GetChild(0).GetComponent<Animator>();
        bodyAnimator = transform.GetChild(1).GetComponent<Animator>();
        legsAnimator = transform.GetChild(2).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) &&
            !Input.GetKey(KeyCode.RightArrow))
        {
            legsAnimator.SetBool("WalkUp", false);
            legsAnimator.SetBool("WalkLeft", false);
            legsAnimator.SetBool("WalkDown", false);
            legsAnimator.SetBool("WalkRight", false);
        }
        
        //rigidbody2D.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) // UP
        {
            rigidbody2D.position += Vector2.up/40;
            legsAnimator.SetBool("WalkUp", true);
        }
        if (Input.GetKey(KeyCode.A)) // LEFT
        {
            rigidbody2D.position += Vector2.left/40;
            legsAnimator.SetBool("WalkLeft", true);
        }
        if (Input.GetKey(KeyCode.S)) // DOWN
        {
            rigidbody2D.position += Vector2.down/40;
            legsAnimator.SetBool("WalkDown", true);
        }
        if (Input.GetKey(KeyCode.D)) // RIGHT
        {
            rigidbody2D.position += Vector2.right/40;
            legsAnimator.SetBool("WalkRight", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Collect")
        {
            AudioManager.instance.PlaySFX("Collect");
        }
    }
}
