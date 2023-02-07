using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health = 12;
    public int maxHealth = 12;
    public string healthString;
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            AudioManager.instance.PlaySFX("Death");
            if (gameObject.CompareTag("Player"))
                SceneManager.LoadScene(0);
            else
                Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == healthString)
        {
            health--;
        }
    }
}
