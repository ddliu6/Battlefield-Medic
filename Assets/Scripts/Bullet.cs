using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = -3f;
    public Rigidbody2D rb;
   
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(other.gameObject.GetComponent<Player>().ShowCurHealth() > 0)
                other.gameObject.GetComponent<Player>().TakeDamage(1);
            
            Destroy(gameObject);
        }
        else if(other.tag == "Ally")
        {
            if (other.gameObject.GetComponent<Ally>().ShowCurHealth() > 0)
                other.gameObject.GetComponent<Ally>().TakeDamage(1);

            Destroy(gameObject);
        }
        else if(other.name == "Tilemap")
        {
            Destroy(gameObject);
        }
        
    }
}
