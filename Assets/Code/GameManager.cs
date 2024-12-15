using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static Action OnCrossRoadEvent; // evento per gestire l'inizio e la fine della scelta di direzione
    public static Action OnUpdateScore;
    public static Action OnGameEnd;

    static int score;
    int maxScore = 10;

    public static int Score { get => score; private set => score = value; } // proprietà per accedere allo score ma non modificarlo

    private void Awake()
    {
        Application.targetFrameRate = 100;
        score = 0;
    }

    private void OnEnable()
    {
        OnCrossRoadEvent += SlowTime;
        OnUpdateScore += IncreaseScore;
    }
    private void OnDisable()
    {
        OnCrossRoadEvent -= SlowTime;
        OnUpdateScore -= IncreaseScore;
    }

    void IncreaseScore()
    {
        if (score < maxScore) score++;
        else OnGameEnd?.Invoke(); //TODO: IMPLEMENTARE LOGICA DI FINE GIOCO
        Debug.Log(score.ToString());
        
    }

    void SlowTime()
    {
        if (Time.timeScale == 1) Time.timeScale = 0.5f; // se l'evento è iniziato và in slowmo,altrimenti torna a velocità normale
        else Time.timeScale = 1;
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
    LEFT, STRAIGHT, RIGHT
}