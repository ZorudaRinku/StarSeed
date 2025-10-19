using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCombat : MonoBehaviour
{
    public float shootCooldownDuration = 0.75f;
    public GameObject arrowPrefab;
    public int arrowSpeedMultiplier = 2;

    public float currentCooldownTimer = 0f;

    private Animator armsAnimator;

    // Start is called before the first frame update
    void Start()
    {
        armsAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update cooldown timer
        if (currentCooldownTimer > 0)
        {
            currentCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow)) // Shoot Up
        { 
            if (CanShoot())
            {
                ShootArrow(Vector2.up, new Vector3(0, 0, 45), new Vector3(0, 1));
            }
        }
        
        if (Input.GetKey(KeyCode.LeftArrow)) // Shoot Left
        {
            if (CanShoot())
            {
                ShootArrow(Vector2.left, new Vector3(0, 0, 135), new Vector3(-0.5f, 0.25f));
            }
        }

        if (Input.GetKey(KeyCode.DownArrow)) // Shoot Down
        {
            if (CanShoot())
            {
                ShootArrow(Vector2.down, new Vector3(0, 0, -135), new Vector3(0, -1.5f));
            }
        }

        if (Input.GetKey(KeyCode.RightArrow)) // Shoot Right
        {
            if (CanShoot())
            {
                ShootArrow(Vector2.right, new Vector3(0, 0, -45), new Vector3(0.5f, 0.3f));
            }
        }
    }

    private bool CanShoot()
    {
        return currentCooldownTimer <= 0;
    }

    private void ShootArrow(Vector2 direction, Vector3 rotation, Vector3 positionOffset)
    {
        AudioManager.instance.PlaySFX("Bow");
        GameObject newArrow = Instantiate(arrowPrefab);
        newArrow.GetComponent<Rigidbody2D>().linearVelocity = direction * arrowSpeedMultiplier;
        newArrow.transform.eulerAngles = rotation;
        newArrow.transform.position = transform.position + positionOffset;
        currentCooldownTimer = shootCooldownDuration;
    }
}
