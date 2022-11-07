using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    EnemyScript enemy;
    PlayerScript player;
    float attackDamage = 20f;
    
    // Start is called before the first frame update
    void Start()
    {

    }


    void OnTriggerStay2D(Collider2D col)
    {

        if ((col.gameObject.tag == "enemy") && player.attackReady)
        {
            enemy = col.gameObject.GetComponent<EnemyScript>();

            enemy.TakeDamage(attackDamage);
            player.attackReady = false;
            print("collision with " + col.gameObject.tag);
        }
    }
    
}
