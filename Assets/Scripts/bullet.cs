using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //public Rigidbody2D rb;
    public float speed = 20f;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public bool flip = false;

    
    
    public void Shoot(Vector2 shootpos, Transform firepoint)
    {
        GameObject Weapon = Instantiate(gameObject, firepoint.position, Quaternion.identity);
        shootpos.Normalize();
        Weapon.GetComponent<Rigidbody2D>().velocity = direction * speed;//new Vector2(0, position.y) * 5f;
        if (flip)
            Weapon.transform.Rotate(0f, 180f, 0f);
        else Weapon.transform.Rotate(0f, 0f, Mathf.Atan2(shootpos.y, shootpos.x) * Mathf.Rad2Deg);
        Destroy(Weapon, 4f);
        flip = false;
        //Weapon.GetComponent<Rigidbody2D>().AddForce(mousePos * 5f, ForceMode2D.Impulse);
        //rb.AddForceAtPosition(mousePos, mousePos);
        //Weapon.GetComponent<Rigidbody2D>().velocity = mousePos * 5;
    }

    void EnemyShoot(Transform pos)
    {
        Instantiate(gameObject, pos.position, Quaternion.identity);
        

    }

    //void Start()
    //{
    //    rb.velocity = transform.right * speed;
    //    //rb.AddForce(player.up * speed);
    //    rb.velocity = new Vector2(speed, 0);
    //}

    //public void Shoot(Vector2 shootingpos)
    //{
    //    //shootingpos.Normalize();

    //}


    //public void Shoot(Vector2 pos)
    //{
    //    //Instantiate(gameObject, firepoint.position, arrow.rotation);
    //    if (pos.x < 0)
    //     rb.velocity = -transform.right * speed;
    //    else if(pos.x > 0)
    //      rb.velocity = transform.right * speed;
    //    else if (pos.y < 0)
    //        rb.velocity = -transform.up * speed;
    //    else if (pos.y > 0)
    //        rb.velocity = transform.up * speed;

    //}

}
