using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject ally;
    public Text scoreText, recordText;
    public int maxAllies = 5;
    public List<bool> isOccupied = new List<bool>();
    public DontDestory record;
    int currentAllies, pos, score = 0, bestScore = 0;
    AudioSource source;

    void Start()
    {
        bestScore = record.ShowBestScore();
        recordText.text = "Best:" + bestScore;

        source = GetComponent<AudioSource>();

        for (int i = 0; i < spawnPoints.Length; ++i)
            isOccupied.Add(false);

        currentAllies = maxAllies;

        for (int i = 0; i < maxAllies; )
        {
            pos = Random.Range(0, spawnPoints.Length);
            if(!isOccupied[pos])
            {
                Instantiate(ally, spawnPoints[pos].position, spawnPoints[pos].rotation);
                ++i;
                isOccupied[pos] = true;
            }
        }
        InvokeRepeating("CheckRespawn", 3f, 1f);
    }

    void Update()
    {
        scoreText.text = "Score:" + score;
    }

    public void GetScore()
    {
        ++score;
        source.Play();
    }

    public int ShowScore()
    {
        return score;
    }

    public void DecreaseAlly()
    {
        currentAllies--;
    }

    public int ShowCurAllies()
    {
        return currentAllies;
    }

    public void RecordScore()
    {
        recordText.text = "Best:" + score;
    }

    void CheckRespawn()
    {
        if (currentAllies < 5)
        {
            for (int i = 0; i < 1;)
            {
                pos = Random.Range(0, spawnPoints.Length);
                if (!isOccupied[pos])
                {
                    StartCoroutine(Delay());
                    Instantiate(ally, spawnPoints[pos].position, spawnPoints[pos].rotation);
                    ++i;
                    isOccupied[pos] = true;
                }
            }
            currentAllies++;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
    }
}
