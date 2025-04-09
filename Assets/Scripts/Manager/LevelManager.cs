using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LevelSO[] gamemodeList; // hold all level of gamemode
    public UnitSO[] units;
    public UnitAttriBute[][][] levelList; // Gamemode, Level, Unit
    public UnitAttriBute[] playerAttribute; // Gamemode, Level, Unit
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
        int totalLevel = gamemodeList[0].levelPercent.Length;
        for (int i = 0; i < totalGameMode; i++)
        {
            levelList[i] = new UnitAttriBute[totalLevel][];
            for (int j = 0; j < totalLevel; j++)
            {
                levelList[i][j] = new UnitAttriBute[units.Length];
                for (int k = 0; k < units.Length; k++)
                {
                    levelList[i][j][k] = CreateUnitAttribute(i, j, k,Faction.Enemy);
                }
            }
        }
        playerAttribute = new UnitAttriBute[units.Length];
        for(int i=0; i<units.Length; i++){
            playerAttribute[i] = CreateUnitAttribute(0,-1,i,Faction.Player);
        }
    }
    public UnitAttriBute GetUnitData(int gamemode,int level, int enemyType)
    {
        if(levelList[gamemode][level][enemyType] == null){
            Debug.LogError("Unit data is null, please check the level and enemyType");
        }
        return levelList[gamemode][level][enemyType];
    }
    public UnitAttriBute GetPlayerData(int playerType)
    {
        if(playerAttribute[playerType] == null){
            Debug.LogError("Player data is null, please check the playerType");
        }
        return playerAttribute[playerType];
    }
    private UnitAttriBute CreateUnitAttribute(int gamemode,int level, int enemyType,Faction faction)
    {
        float levelScale = GetLevelScale(gamemode, level);
        UnitSO unitSO = units[enemyType];
        float health = UnitBaseAttribute.health * unitSO.healthScale * UnityEngine.Random.Range(underRandomScale, upperRandomScale)*levelScale;
        float speed = UnitBaseAttribute.speed * unitSO.speedScale * UnityEngine.Random.Range(underRandomScale, upperRandomScale)*levelScale;
        float damage = UnitBaseAttribute.damage * unitSO.damageScale * UnityEngine.Random.Range(underRandomScale, upperRandomScale)*levelScale;
        return new UnitAttriBute(health, speed, damage,faction);
    }
    private float GetLevelScale(int gamemode, int level)
    {
        if(level == -1){
            return 1;
        }
        return gamemodeList[gamemode].levelPercent[level];
    }
    public int GetEnemyCount(int gamemode, int level)
    {
        if (gamemodeList[gamemode].isManyEnemy == false)
        {
            return 1;
        }
        return gamemodeList[gamemode].enemyCount[level];
    }
}
