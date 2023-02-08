using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleHealth : MonoBehaviour
{
    public int health = 12;
    public string healthString;
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            AudioManager.instance.PlaySFX("Death");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(healthString))
            health--;
    }
}
