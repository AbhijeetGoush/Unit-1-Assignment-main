using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    

    PlayerScript player;
    [SerializeField] public float currentHealth, maxHealth = 100f;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            anim.SetBool("SlimeDeath", true);
            Destroy(this.gameObject);

        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        print("enemy hit. Damage=" + currentHealth);

    }
    
    
    
}
