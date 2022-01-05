using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumPads : MonoBehaviour
{

    public float bounce = 20f;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}
