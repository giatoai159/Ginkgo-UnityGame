using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsCloud : MonoBehaviour
{
    
      public float bounce = 38f;
    // Start is called before the first frame update
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*bounce,ForceMode2D.Impulse);
        }
    }
    */
    private  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.up*bounce,ForceMode2D.Impulse);
        }
    }
}
