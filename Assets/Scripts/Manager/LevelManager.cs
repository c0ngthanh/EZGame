using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LevelSO[] levels;
    public UnitSO[] units;
    public UnitAttriBute[][][] levelList; // Gamemode, Level, Unit
    private float underRandomScale = 0.9f;
    private float upperRandomScale = 1.1f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        levelList = new UnitAttriBute[Enum.GetNames(typeof(GameMode)).Length][][];
        int totalGameMode = Enum.GetNames(typeof(GameMode)).Length;
        int totalLevel = levels.Length;
        for (int i = 0; i < totalGameMode; i++)
        {
            levelList[i] = new UnitAttriBute[totalLevel][];
            for (int j = 0; j < totalLevel; j++)
            {
                levelList[i][j] = new UnitAttriBute[units.Length];
            }
        }
    }
    public UnitAttriBute GetUnitData(int gamemode,int level, int enemyType)
    {
        if(levelList[gamemode][level][enemyType] == null){
            levelList[gamemode][level][enemyType] = CreateUnitAttribute(gamemode, level, enemyType);
        }
        return levelList[gamemode][level][enemyType];
    }
    private UnitAttriBute CreateUnitAttribute(int gamemode,int level, int enemyType)
    {
        UnitSO unitSO = units[enemyType];
        float health = UnitBaseAttribute.health * unitSO.healthScale * UnityEngine.Random.Range(underRandomScale, upperRandomScale);
        float speed = UnitBaseAttribute.speed * unitSO.speedScale * UnityEngine.Random.Range(underRandomScale, upperRandomScale);
        float damage = UnitBaseAttribute.damage * unitSO.damageScale * UnityEngine.Random.Range(underRandomScale, upperRandomScale);
        return new UnitAttriBute(health, speed, damage);
    }
}
