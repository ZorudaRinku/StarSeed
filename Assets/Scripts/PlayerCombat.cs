using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator armsAnimator, bodyAnimator, legsAnimator;

    public float Cooldown = 30;
    private float CooldownCount = 0;
    public GameObject Arrow;
    

    public int ArrowSpeedMultiplier = 2;
    // Start is called before the first frame update
    void Start()
    {
        //animation = GetComponent<Animation>();
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
            armsAnimator.SetBool("ShootUp", false);
            armsAnimator.SetBool("ShootLeft", false);
            armsAnimator.SetBool("ShootDown", false);
            armsAnimator.SetBool("ShootRight", false);
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow)) // Shoot Up
            {
                armsAnimator.SetBool("ShootUp", true);
                bodyAnimator.SetBool("Up", true);
                if (CooldownCount >= Cooldown)
                {
                    AudioManager.instance.PlaySFX("Bow");
                    GameObject arrow = Instantiate(Arrow);
                    arrow.GetComponent<Rigidbody2D>().velocity += Vector2.up * ArrowSpeedMultiplier;
                    arrow.transform.eulerAngles = new Vector3(0, 0, 90);
                    arrow.transform.position = transform.position + new Vector3(0, 1);
                    CooldownCount = 0;

                }
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                armsAnimator.SetBool("ShootLeft", true);
                bodyAnimator.SetBool("Left", true);
                if (CooldownCount >= Cooldown)
                {
                    AudioManager.instance.PlaySFX("Bow");
                    GameObject arrow = Instantiate(Arrow);
                    arrow.GetComponent<Rigidbody2D>().velocity += Vector2.left * ArrowSpeedMultiplier;
                    arrow.transform.eulerAngles = new Vector3(0, 0, 180);
                    arrow.transform.position = transform.position + new Vector3(-1, 0);
                    CooldownCount = 0;
                }
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                armsAnimator.SetBool("ShootDown", true);
                bodyAnimator.SetBool("Down", true);
                if (CooldownCount >= Cooldown)
                {
                    AudioManager.instance.PlaySFX("Bow");
                    GameObject arrow = Instantiate(Arrow);
                    arrow.GetComponent<Rigidbody2D>().velocity += Vector2.down * ArrowSpeedMultiplier;
                    arrow.transform.eulerAngles = new Vector3(0, 0, -90);
                    arrow.transform.position = transform.position + new Vector3(0, -1);
                    CooldownCount = 0;
                }
            }
            
            if (Input.GetKey(KeyCode.RightArrow))
            {

                armsAnimator.SetBool("ShootRight", true);
                bodyAnimator.SetBool("Right", true);
                if (CooldownCount >= Cooldown)
                {
                    AudioManager.instance.PlaySFX("Bow");
                    GameObject arrow = Instantiate(Arrow);
                    arrow.GetComponent<Rigidbody2D>().velocity += Vector2.right * ArrowSpeedMultiplier;
                    arrow.transform.eulerAngles = new Vector3(0, 0, 0);
                    arrow.transform.position = transform.position + new Vector3(1, 0);
                    CooldownCount = 0;

                }
            }
            CooldownCount = CooldownCount + (1 * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
