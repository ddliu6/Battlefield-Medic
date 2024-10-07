using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestory : MonoBehaviour
{
    static DontDestory instance;
    static int bestScore = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    public int ShowBestScore()
    {
        return bestScore;
    }

    public void SetBestScore(int score)
    {
        bestScore = score;
    }
}
