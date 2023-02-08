using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BoltLifetime : MonoBehaviour
{
    [SerializeField] private float boltSpeed = 3f;

    [SerializeField] private float boltLifetime = 3f;

    private float _boltLifetimeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -boltSpeed * Time.deltaTime, 0);
        if (_boltLifetimeCount >= boltLifetime)
        {
            Vector3 velocity = Vector3.zero;
            transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.zero, ref velocity, .05f);
            boltSpeed = Vector3.SmoothDamp(new Vector3(0, boltSpeed , 0), Vector3.zero, ref velocity, 0.2f).y;
        }
        if (transform.localScale.magnitude < 0.01f)
            Destroy(gameObject);
        
        _boltLifetimeCount = _boltLifetimeCount + (1 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<HealthManager>().HealthSystem.Damage(1);
            Destroy(gameObject);
        }
        if (col.CompareTag("Projectile") || col.CompareTag("Wall"))
            // TODO: Play splash animation
            Destroy(gameObject);
    }
}
