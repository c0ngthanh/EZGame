using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelSO", order = 1)]
public class LevelSO : ScriptableObject
{
    // Increase % from base level
    public float[] levelPercent = new float[10];
    public bool isManyEnemy = false; // if true, enemy will spawn in wave
    public int[] enemyCount = new int[10]; 
}
