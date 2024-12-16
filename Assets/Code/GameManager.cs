using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    
    public static Action OnUpdateScore;
    public static Action OnGameEnd;

    static int score;
    int maxScore = 1;

    public static int Score { get => score; private set => score = value; } // propriet� per accedere allo score ma non modificarlo

    private void Awake()
    {
        Application.targetFrameRate = 100;
        score = 0;
    }

    private void OnEnable()
    {
        
        OnUpdateScore += IncreaseScore;
    }
    private void OnDisable()
    {
        
        OnUpdateScore -= IncreaseScore;
    }

    void IncreaseScore()
    {
        score++;
        if (score == maxScore) StartCoroutine(Timer());
    }
       
        

    void SlowTime() // NON IMPLEMENTATA LA MECCANICA RELATIVA
    {
        if (Time.timeScale == 1) Time.timeScale = 0.5f; // se l'evento � iniziato v� in slowmo,altrimenti torna a velocit� normale
        else Time.timeScale = 1;
    }

    IEnumerator Timer()
    {
        
        float timeElapsed = 0;
        while (timeElapsed < 2)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        OnGameEnd?.Invoke();
    }
}

public enum Entrance
{
    NORTH,
    SOUTH,
    EAST,
    WEST
}
public enum Direction
{
    LEFT,
    STRAIGHT,
    RIGHT
}
