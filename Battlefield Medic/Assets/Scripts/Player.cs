using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    public int maxHealth = 10;
    private int currentHealth = 0;
    public HealthBar healthBar;
    public Sprite playerDead;
    AudioSource source;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessInputs();
        CheckGameOver();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public int ShowCurHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        source.Play();
    }

    void CheckGameOver()
    {
        if (currentHealth < 1)
        {
            GetComponent<SpriteRenderer>().sprite = playerDead;
            rb.velocity = new Vector2();
            enabled = false;

            RandomSpawner curScore = GameObject.Find("Random Spawner").GetComponent<RandomSpawner>();
            DontDestory recordScore = GameObject.Find("Record").GetComponent<DontDestory>();
            Debug.Log("Cur: " + curScore.ShowScore() + " Record: " + recordScore.ShowBestScore());
            if (curScore.ShowScore() > recordScore.ShowBestScore())
            {
                curScore.RecordScore();
                recordScore.SetBestScore(curScore.ShowScore());
            }

            Invoke("Restart", 1f);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
