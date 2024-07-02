using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Destroy(gameObject, 4);
    }

    private void Update()
    {
        //transform.Translate(0, speed * Time.deltaTime, 0);
        //transform.Translate(transform.forward * speed);
        rb.AddForce(rb.transform.forward * 50 * Time.deltaTime);
        //rb.velocity = transform.right * 15f;

        Destroy(this.gameObject, 1f);
    }

    
}
