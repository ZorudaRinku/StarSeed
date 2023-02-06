using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLife : MonoBehaviour
{
    public int lifetime = 4;

    private float lifetimeCount = 0;

    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetimeCount >= lifetime)
            Destroy(gameObject);

        if (_rigidbody2D.velocity.magnitude < 1)
            Destroy(gameObject);

        lifetimeCount += lifetimeCount * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
