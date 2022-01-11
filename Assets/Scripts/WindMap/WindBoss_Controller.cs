using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBoss_Controller : MonoBehaviour
{
    // Start is called before the first frame update
     public float speed = 1.5f;
    public bool vertical;
    Rigidbody2D rb2d;
    int direction =1;
    float movingTimer;
    public float movingTime = 1.5f;
    Animator animator;
    bool broken = true;
    public ParticleSystem smokeEffect;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movingTimer = movingTime;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetTrigger("WIndBoss_Idle");

        if(!broken)
        {
            return;
        }
        

        movingTimer -= Time.deltaTime;
        if(movingTimer<0)
        {
            direction *= -1;
            movingTimer = movingTime;
        }
       // animator.SetTrigger("WIndBoss_Idle");
        //Set animation
        
        if(vertical)
        {
            animator.SetFloat("Move X",0);
            animator.SetFloat("Move Y",direction);
        }
        else
        {
            animator.SetFloat("Move X",direction);
            animator.SetFloat("Move Y",0);
        }
        
    }

    void FixedUpdate()
    {
        
         if(!broken)
        {
            return;
        }
        

        Vector2 pos = rb2d.position;
        if(vertical)
        {
            pos.y += (speed*Time.deltaTime*direction);
        }
        else
        {
            pos.x += (speed*Time.deltaTime*direction);
        }
        rb2d.MovePosition(pos);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthController controller = collision.GetComponent<PlayerHealthController>();
        if(controller != null)
        {
            controller.ChangeHealth(-1); 
            //controller.PlaySound(controller.hitSound);
        }
    }
/*
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController controller = other.collider.GetComponent<RubyController>();
        if(controller != null)
        {
            controller.ChangeHealth(-1); 
        }
        Debug.Log("Check");
    }
*/
/*
    public void Fix()
    {
        broken = false;
        rb2d.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
    */
}
