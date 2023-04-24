using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Game Settings")]
[System.Serializable]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    public int moneyAmount = 0;
    [SerializeField]
    public int waveCount = 0;
    [SerializeField]
    public int health = 0;
    [SerializeField]
    public int score = 0;

    //clear the values whenever you move to the next scene
    public void ClearValues()
    {
        moneyAmount = 0;
        waveCount = 0;
        health = 0;
        score = 0;
    }
    //intiialize the values when the level starts
    public void InitializeValues(int initialMoney, int initialWaves, int initialHealth, int initialScore)
    {
        moneyAmount = initialMoney;
        waveCount = initialWaves;
        health = initialHealth;
        score = initialScore;
    }
}
