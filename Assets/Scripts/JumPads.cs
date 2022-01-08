using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumPads : MonoBehaviour
{

    public float bounce = 20f;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collide");
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}
