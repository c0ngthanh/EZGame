using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelSO[] levels;
    public UnitSO[] units;
    public Dictionary<UnitSO, UnitAttriBute> unitDictionary = new Dictionary<UnitSO, UnitAttriBute>();
    private float underRandomScale = 0.9f;
    private float upperRandomScale = 1.1f;
    private int currentLevel = 0;
    public void CalculateLevelAttributes()
    {
        for(int i = 0; i < units.Length; i++)
        {
            float health = UnitBaseAttribute.health * levels[currentLevel].levelPercent[currentLevel] * units[i].healthScale * Random.Range(underRandomScale, upperRandomScale);
            float speed = UnitBaseAttribute.speed * levels[currentLevel].levelPercent[currentLevel] * units[i].speedScale * Random.Range(underRandomScale, upperRandomScale);
            float damage = UnitBaseAttribute.damage * levels[currentLevel].levelPercent[currentLevel] * units[i].damageScale * Random.Range(underRandomScale, upperRandomScale);
            unitDictionary[units[i]] = new UnitAttriBute(health, speed, damage);
        }
    }
}
