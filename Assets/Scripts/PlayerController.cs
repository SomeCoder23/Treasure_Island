using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    #region Singleton
    public static PlayerController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Player Controller!");
            return;
        }

        instance = this;
    }

    #endregion
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public AudioClip[] eatSounds, hitSounds;
    //new public AudioSource audio;
    //public HealthBar healthbar;
    public Transform shootPoint;
    public bullet arrow;
    public Camera cam;
    public AudioClip coinSound, collectSound;

    private Vector2 position, mousePos;
    //private int health = 100; 

    //[HideInInspector]
    //public int currentHealth = 100; 
    [HideInInspector]
    public bool gatherQuest = false, killQuest = false;
    [HideInInspector]
    public Quest quest;

    //private void Start()
    //{
    //    healthbar.SetMaxHealth(MaxHealth);
    //    healthbar.SetHealth(MaxHealth);

    //}

    void Update()
    {
        position.x = Input.GetAxisRaw("Horizontal");
        position.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        animator.SetFloat("WalkY", position.x);
        animator.SetFloat("WalkX", position.y);
        animator.SetFloat("Speed", position.sqrMagnitude);

        if (position.x > 0 || mousePos.x > transform.localPosition.x )
            GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;

        if (Input.GetButtonDown("Fire1"))
        {
            //SoundManager.instance.PlayOnce(runningAwayAudio);
            if (position.x == 0 && position.y == 0){
                if (mousePos.x > transform.localPosition.x)
                    arrow.direction = transform.right;
                else{
                    arrow.direction = -transform.right;
                    arrow.flip = true;
                }
            }
            else arrow.direction = position;
            arrow.Shoot(position, shootPoint); 
        }
    }

    private void FixedUpdate()
    {
        position.Normalize();
        rb.MovePosition(rb.position + position * speed * Time.fixedDeltaTime);
        //Vector2 lookDir = mousePos - rb.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon-")
        {
            SoundManager.instance.RandomClip(hitSounds);
            Destroy(collision.gameObject);
            PlayerUI.instance.currentHealth -= 5;
            PlayerUI.instance.healthbar.SetHealth(PlayerUI.instance.currentHealth);
            if (PlayerUI.instance.currentHealth <= 0)
                PlayerUI.instance.Die();
        }

        if (collision.gameObject.tag == "Food" || collision.gameObject.tag == "collectable" || collision.gameObject.tag == "Potion") {
            if (Inventory.instance.Add(collision.gameObject)) {
                collision.gameObject.SetActive(false);
                SoundManager.instance.PlayOnce(collectSound);
                //if (gatherQuest)
                //    quest.Evaluate(collision.gameObject.GetComponent<SpriteRenderer>().sprite);

            }
        }

        if(collision.gameObject.tag == "Gold") {
            Destroy(collision.gameObject);
            PlayerUI.coins += 100;
            SoundManager.instance.PlayOnce(coinSound);
        }
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            SoundManager.instance.PlayOnce(coinSound);
            PlayerUI.coins += 5;
        }
        PlayerUI.instance.coinCount.text = PlayerUI.coins.ToString();
    }

    public void Eat()
    {
        SoundManager.instance.RandomClip(eatSounds);
        if (PlayerUI.instance.currentHealth < 100)
        {
            PlayerUI.instance.currentHealth += 5;
            PlayerUI.instance.healthbar.SetHealth(PlayerUI.instance.currentHealth);
        }
    } 
    
    public void Kill(GameObject victim)
    {
          Debug.Log("Player became a murderer 0o0!");
          PlayerUI.XP += 10;
          PlayerUI.instance.experienceCount.text = PlayerUI.XP.ToString();
          if (killQuest)
              quest.EvaluateKill(victim.GetComponent<SpriteRenderer>().sprite);
    }

    
}


  //void Shoot()
    //{
    //    GameObject Arrow = Instantiate(arrow, firepoint.position, Quaternion.identity);
    //    Vector2 shootpos = position;
    //    shootpos.Normalize();
    //    Arrow.GetComponent<Rigidbody2D>().velocity = -transform.right * 14f;//new Vector2(0, position.y) * 5f;
    //    Arrow.transform.Rotate(0f, 0f, Mathf.Atan2(shootpos.y, shootpos.x) * Mathf.Rad2Deg);
    //    Destroy(Arrow, 4f);

    //    //Arrow.GetComponent<Rigidbody2D>().AddForce(mousePos * 5f, ForceMode2D.Impulse);
    //    //rb.AddForceAtPosition(mousePos, mousePos);
    //    //Arrow.GetComponent<Rigidbody2D>().velocity = mousePos * 5;
    //}

    //if (position.x > 0 && transform.rotation.eulerAngles.y == 0)
    //    transform.Rotate(0f, 180f, 0f);
    ////GetComponent<SpriteRenderer>().flipX = true;
    //else if(position.x < 0 && transform.rotation.eulerAngles.y == 180)
    //{
    //    transform.Rotate(0f, 180f, 0f);
    //}
    //GetComponent<SpriteRenderer>().flipX = false;

