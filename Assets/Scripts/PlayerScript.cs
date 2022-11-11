using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    bool touchingplatform;
    public float health;
    private Animator anim;
    public bool attack;
    public bool run;
    HelperScript helper;
    EnemyScript enemy;
    PlayerScript player;
    public float attackDamage = 20f;
    public bool attackReady;
    public int PlayerCoin;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        touchingplatform = false;
        health = 3;
        anim = GetComponent<Animator>();
        attack = false;
        helper = gameObject.AddComponent<HelperScript>();
        PlayerCoin = 0;
    }

    // Update is called once per frame
    public async void Update()
    {
        anim.SetBool("Run", false);
        
        Vector2 vel = rb.velocity;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = 7;
            anim.SetBool("Run", true);
            run = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -7;
            anim.SetBool("Run", true);
            run = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && (touchingplatform == true))
        {
            vel.y = 7;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) 
        {
            await Task.Delay(50);
            vel.x = 0;
            
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            await Task.Delay(50);
            vel.x = 0;
        }
        if(health <= 0)
        {
            anim.SetBool("Run", false);
            anim.SetBool("Attack", false);
            anim.SetBool("Die", true);
            await Task.Delay(1000);
            Destroy(this.gameObject);
            SceneManager.LoadScene(3);

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            attackReady = true;
            anim.SetBool("Attack", true);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            attackReady = false;
            anim.SetBool("Attack", false);
        }
        if(Input.GetKey("left"))
        {
            helper.FlipObject(true);
        }
        if (Input.GetKey("right"))
        {
            helper.FlipObject(false);
        }
        
        rb.velocity = vel;
    
    }

    async public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            touchingplatform = true;
            
        }
        if (collision.gameObject.tag == "Chest" && PlayerCoin == 1)
        {
            
            await Task.Delay(1000);
            Destroy(this.gameObject);
            SceneManager.LoadScene(2);
        }
        if (collision.gameObject.tag == "Water")
        {
            SceneManager.LoadScene(3);
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            touchingplatform = false;
            
            
        }
    }


     public void OnTriggerStay2D(Collider2D col)
    {

        if ((col.gameObject.tag == "enemy") && attackReady)
        {
            enemy = col.gameObject.GetComponent<EnemyScript>();

            enemy.TakeDamage(attackDamage);
            player.attackReady = false;
            print("collision with " + col.gameObject.tag);
        }
        if (col.gameObject.tag == "Coin")
        {
            PlayerCoin = 1;
        }
        
    }
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        while (collision.gameObject.tag == "enemy")
        {
            health -= 1;
            await Task.Delay(3000);
        }
        

    }
    void StartOfAttack()
    {
        attackReady = true;
    }
    void EndOfAttack()
    {
        attackReady = false;
    }
}
