using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowLife : MonoBehaviour
{
    public int lifetime = 4;
    public int arrowHealth = 2;

    private float lifetimeCount = 0.1f;
    private float _alpha;
    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _alpha = GetComponent<Renderer>().material.color.a;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetimeCount >= lifetime)
            _spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(_spriteRenderer.color.a, 0f, Time.deltaTime / 1));
        
        if (arrowHealth <= 0 || _spriteRenderer.color.a <= 0.01)
            Destroy(gameObject);

        lifetimeCount += lifetimeCount * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
            Destroy(gameObject);
        if (col.CompareTag("EnemyProjectile"))
            arrowHealth--;
        if (col.CompareTag("Wall"))
            _rigidbody2D.velocity = Vector2.zero;
    }
}
