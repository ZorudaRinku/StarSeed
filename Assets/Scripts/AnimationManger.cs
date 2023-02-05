using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimationManger : MonoBehaviour
{
    private Animator armsAnimator, bodyAnimator, legsAnimator;

    private string _direction = "Right";
    private bool _shooting = false;
    private bool _moving = false;

    // Start is called before the first frame update
    void Start()
    {
        armsAnimator = transform.GetChild(0).GetComponent<Animator>();
        bodyAnimator = transform.GetChild(1).GetComponent<Animator>();
        legsAnimator = transform.GetChild(2).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        UpdateAnimation();
    }
    
    void CheckMovement()
    {
        if (!checkWASD() && !checkArrows())
        {
            Debug.Log(armsAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1);
            if (armsAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 > .9f && armsAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 1f)
                armsAnimator.speed = 0;
            _moving = false;
            //ResetAnimation();
        }
        else // WASD or Arrows
        {
            _shooting = checkArrows();
            _moving = checkWASD();
            if (checkArrows()) // Arrows
            {
                armsAnimator.speed = 1;
                if (Input.GetKey(KeyCode.UpArrow)) // Shoot Up
                    _direction = "Up";

                if (Input.GetKey(KeyCode.LeftArrow)) // Shoot Left
                        _direction = "Left";

                if (Input.GetKey(KeyCode.DownArrow)) // Shoot Down
                    _direction = "Down";

                if (Input.GetKey(KeyCode.RightArrow)) // Shoot Right
                    _direction = "Right";
            }
            else if (checkWASD())
            {
                if (Input.GetKey(KeyCode.W)) // Move Up
                    _direction = "Up";

                if (Input.GetKey(KeyCode.A)) // Move Left
                    _direction = "Left";
                
                if (Input.GetKey(KeyCode.S)) // Move Down
                    _direction = "Down";

                if (Input.GetKey(KeyCode.D)) // Move Right
                    _direction = "Right";
            }
        }
    }

    private void UpdateAnimation()
    {
        if (_direction == "Up")
        {
            if (_shooting)
                armsAnimator.Play("ShootU");
            else
                armsAnimator.Play("IdleU");
            
            bodyAnimator.Play("BodyIdleU");
            
            if (_moving)
                legsAnimator.Play("LegsWalkU");
            else
                legsAnimator.Play("LegsIdleU");
        }
        else if (_direction == "Left")
        {
            if (_shooting)
                armsAnimator.Play("ShootL");
            else
                armsAnimator.Play("IdleL");
            
            bodyAnimator.Play("BodyIdleL");
            
            if (_moving)
                legsAnimator.Play("LegsWalkL");
            else
                legsAnimator.Play("LegsIdleL");
        }
        else if (_direction == "Down")
        {
            if (_shooting)
                armsAnimator.Play("ShootD");
            else
                armsAnimator.Play("IdleD");
            
            bodyAnimator.Play("BodyIdleD");
            
            if (_moving)
                legsAnimator.Play("LegsWalkD");
            else
                legsAnimator.Play("LegsIdleD");
        }
        else if (_direction == "Right")
        {
            if (_shooting)
                armsAnimator.Play("ShootR");
            else
                armsAnimator.Play("IdleR");
            
            bodyAnimator.Play("BodyIdleR");
            
            if (_moving)
                legsAnimator.Play("LegsWalkR");
            else
                legsAnimator.Play("LegsIdleR");
        }
    }

    bool checkArrows()
    {
        return Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) ||
               Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow);
    }
    
    bool checkWASD()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
               Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }
    
    private void ResetAnimation()
    {
        if (_direction == "Up")
        {
            armsAnimator.Play("ShootU", 0, .74f);
            bodyAnimator.Play("BodyIdleU", 0, 0);
            legsAnimator.Play("LegsIdleU", 0, 0);
        }

        if (_direction == "Left")
        {
            armsAnimator.Play("ShootL", 0, .74f);
            bodyAnimator.Play("BodyIdleL", 0, 0);
            legsAnimator.Play("LegsIdleL", 0, 0);
        }

        if (_direction == "Down")
        {
            armsAnimator.Play("ShootD", 0, 1f);
            bodyAnimator.Play("BodyIdleD", 0, 0);
            legsAnimator.Play("LegsIdleD", 0, 0);
        }

        if (_direction == "Right")
        {
            armsAnimator.Play("ShootR", 0, .99f);
            bodyAnimator.Play("BodyIdleR", 0, 0);
            legsAnimator.Play("LegsIdleR", 0, 0);
        }
    }
}
