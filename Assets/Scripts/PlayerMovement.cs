using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 movement = new Vector3(0, 0, 0);

    [SerializeField] private float initialMoveSpeed = 5f;
    private float moveSpeed;
    public int stars = 0;

    private PlayerCombat _playerCombat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (_playerCombat.currentCooldownTimer > 0) // Slow movement when shooting
        {
            moveSpeed = initialMoveSpeed * 0.8f;
        } else
        {
            moveSpeed = initialMoveSpeed;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (stars >= 10)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Collect")
        {
            AudioManager.instance.PlaySFX("Collect");
            stars++;
            Destroy(collision.gameObject);
        }
    }
}
