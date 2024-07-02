using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f, shootDistance = 5f, knifeForce = 20;
    public Rigidbody2D rb;
    public Transform objectCheck;
    public LayerMask mask;
    public Animator animator;
    public GameObject knife;
    public int damage = 10, throwTime = 20;
    //public Vector3 startpos, endpos;
    public AudioClip deathAudio, chasingAudio;
    public AudioClip[] hitSounds;
    public Transform startPosition;

    bool patrol = true, flip = false, killed = false, flipX = false;
    Transform player;
    public float retreatDis = 0.5f;
    int health = 100;
    float shootTime = 20, distance;
    Vector2 lastPos;
    bullet weapon = null;
    int dir = 1;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPos = transform.position;
        animator.SetFloat("Speed", 1);
    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Patrolling code
        if (patrol) {
            animator.SetFloat("WalkY", transform.position.x);
            animator.SetFloat("WalkX", 0);
            if (flip)
                Flip();
            rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
        }

        //Chasing player code
        distance = Vector2.Distance(transform.position, player.position);
        if (distance <= shootDistance && distance >= retreatDis)
        {
            if(chasingAudio != null)
                SoundManager.instance.PlayOnce(chasingAudio);
            //Attack();
            patrol = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, 3 * Time.deltaTime);
            #region stuff
            //if(transform.position.y == player.position.y) {
            //    animator.SetFloat("WalkX", 0);
            //    if (player.position.x < transform.position.x && !flip)
            //        GetComponent<SpriteRenderer>().flipX = true;
            //    else GetComponent<SpriteRenderer>().flipX = false;
            //}
            //else {
            //    animator.SetFloat("WalkY", 0);
            //    if (player.position.y < transform.position.y)
            //        animator.SetFloat("WalkX", -1);
            //    else if (player.position.y > transform.position.y)
            //        animator.SetFloat("WalkX", 1);
            //}
            //if (lastPos.y < transform.position.y /*&& lastPos.x == transform.position.x*/)
            //{
            //    animator.SetFloat("WalkY", 0);
            //    animator.SetFloat("WalkX", 1);
            //}
            //else if (lastPos.y > transform.position.y) {
            //    animator.SetFloat("WalkY", 0);
            //    animator.SetFloat("WalkX", -1);
            //}
            //else if (lastPos.y == transform.position.y)
            //{
            //    animator.SetFloat("WalkX", 0);
            //    animator.SetFloat("WalkY", 1);
            #endregion

            //Flip player and shooting direction while chasing
            if (player.position.x > transform.position.x)
            {
                dir = -1;
                GetComponent<SpriteRenderer>().flipX = flipX;
            } else {
                dir = 1; 
                GetComponent<SpriteRenderer>().flipX = !flipX;
            }
           
            if (shootTime <= 0)
            {
                ShootKnife(dir);
                #region more stuff
                //Vector3 pos = transform.TransformPoint(Vector3.forward * 1.5f);
                //weapon = Instantiate(knife);
                //weapon.transform.position = pos;
                //weapon.transform.Translate(transform.forward * 20);
                //weapon.transform.Rotate(0f, 0f, Mathf.Atan2(player.position.y, player.position.x) * Mathf.Rad2Deg);
                //weapon = Instantiate(knife, pos, knife.transform.rotation);
                //   if (player.transform.position.x > transform.localPosition.x)
                //       knife.direction = transform.right;
                //   else
                //   {
                //       knife.direction = -transform.right;
                //       knife.flip = true;
                //   }


                //else 
                //knife.direction = transform.position;


                //knife.Shoot(transform.position, objectCheck);
                #endregion
                shootTime = throwTime;
            }
            else shootTime--;

            //patrol = true;
        }      
        else if (!patrol)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition.position, 3 * Time.deltaTime);
            if(transform.position == startPosition.position) 
              patrol = true;
        } else 
            GetComponent<SpriteRenderer>().flipX = false;


        if (health <= 0)
        {
            if (!killed)
            {
                if(deathAudio != null)
                    SoundManager.instance.PlayOnce(deathAudio);
                GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0, 15, 86); //Color.Lerp(Color.red, Color.white, 20.0f);
                PlayerController.instance.Kill(gameObject);
                Destroy(gameObject, 1f);
                killed = true;
            }
        }

        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (patrol)
            flip = Physics2D.OverlapCircle(objectCheck.position, 0.1f, mask);
    }


    void Flip()
    {
        patrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        flipX = !flipX;
        //GetComponent<SpriteRenderer>().flipX = !flipX;
        speed *= -1;
        patrol = true;
    }

    void ShootKnife(int dir)
    {
        GameObject weapon = Instantiate(knife, objectCheck.position, knife.transform.rotation);
        weapon.transform.position = transform.position * 1;
        //weapon.transform.Rotate(0f, 0f, Mathf.Atan2(player.position.y, player.position.x) * Mathf.Rad2Deg);
        //weapon.transform.Translate(transform.right * 10f * Time.deltaTime);
        weapon.GetComponent<Rigidbody2D>().velocity = transform.right * knifeForce * dir;
        //weapon.transform.position += player.position;
        //weapon.GetComponent<Rigidbody2D>().AddForce(player.position * 35);
        //Vector2 pos = new Vector2(player.position.x, player.position.y - 90);
        //weapon.GetComponent<Rigidbody2D>().AddForceAtPosition(player.position * 35, pos);
        Destroy(weapon, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Weapon+")
        {
            Debug.Log("Hit Enemy with " + collision.gameObject.name);
            Destroy(collision.gameObject);
            health -= damage;
            if(hitSounds.Length > 0)
                SoundManager.instance.RandomClip(hitSounds);
        }
    }




    //void Attack()
    //{
    //    patrol = false;
    //    transform.position = Vector2.MoveTowards(transform.position, player.position, speed / 2 * Time.deltaTime);
    //    if (timeBetweenShots <= 0)
    //    {
    //        knife.direction = player.position;
    //        knife.Shoot(player.position, objectCheck);
    //        timeBetweenShots = 2;
    //    }
    //    else timeBetweenShots -= Time.deltaTime;

    //}

}

/*float range = radius * radius;
        if (collided)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }

        if (transform.localPosition.sqrMagnitude > range)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }*/




/*if (rb.velocity.x < 0)
{
    animator.SetFloat("WalkY", 0);
    if (player.position.y < transform.position.y)
        animator.SetFloat("WalkX", -1);
    else if (player.position.y > transform.position.y)
        animator.SetFloat("WalkX", 1);
}
else
                //animator.SetFloat("WalkX", 0);
                if (player.position.x < transform.position.x && !flip)
    GetComponent<SpriteRenderer>().flipX = true;
else GetComponent<SpriteRenderer>().flipX = false;*/


//else if (Vector2.Distance(transform.position, player.position) < retreatDis)
//    transform.position = Vector2.MoveTowards(transform.position, player.position, -3 * Time.deltaTime);