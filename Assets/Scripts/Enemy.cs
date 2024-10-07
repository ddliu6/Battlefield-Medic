using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;

    public float moveSpeed = 1f;
    Transform topPoint, downPoint;

    bool moveT = true;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 3f, 1f);
        rb = GetComponent<Rigidbody2D>();
        topPoint = GameObject.Find("Top").GetComponent<Transform>();
        downPoint = GameObject.Find("Down").GetComponent<Transform>();
        if (Random.Range(0, 2) == 1)
            moveT = false;
    }

    void FixedUpdate()
    {
        if (transform.position.y > topPoint.position.y)
            moveT = false;
        if (transform.position.y < downPoint.position.y)
            moveT = true;
        if (moveT)
            moveTop();
        else
            moveDown();
    }

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    void moveTop()
    {
        moveT = true;
        rb.velocity = new Vector2(0, moveSpeed);
    }
    void moveDown()
    {
        moveT = false;
        rb.velocity = new Vector2(0, moveSpeed * -1);
    }
}
