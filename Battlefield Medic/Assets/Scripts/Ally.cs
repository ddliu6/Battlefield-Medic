using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth = 3;
    public HealthBar healthBar;
    private bool canHeal = false;
    private RandomSpawner AllyManager;
    private int allyPos;

    // Start is called before the first frame update
    void Start()
    {
        AllyManager = GameObject.Find("Random Spawner").GetComponent<RandomSpawner>();

        for(int i = 0; i < AllyManager.spawnPoints.Length; ++i)
            if(gameObject.transform.position == AllyManager.spawnPoints[i].position)
                allyPos = i;

        healthBar.SetHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(canHeal && Input.GetKeyDown(KeyCode.Space))
        {
            if (currentHealth < 10)
                currentHealth++;
            else
                currentHealth = maxHealth;

            healthBar.SetHealth(currentHealth);
        }

        if(currentHealth == maxHealth)
        {
            AllyManager.GetScore();
            AllyManager.DecreaseAlly();
            AllyManager.isOccupied[allyPos] = false;
            gameObject.SetActive(false);
        }
        else if(currentHealth == 0)
        {
            AllyManager.DecreaseAlly();
            AllyManager.isOccupied[allyPos] = false;
            gameObject.SetActive(false);
        }
    }

    public int ShowCurHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canHeal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canHeal = false;
        }
    }
}
