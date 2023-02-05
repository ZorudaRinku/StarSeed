using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    int health;
    public string healthString;
    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            if (gameObject.tag == ("Player"))
            {
                AudioManager.instance.PlaySFX("Death");
                SceneManager.LoadScene(0);

            }
            else
            {
                AudioManager.instance.PlaySFX("Death");
                Destroy(gameObject);
            }

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
