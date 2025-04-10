using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSO", menuName = "ScriptableObjects/UnitSO")]
public class UnitSO : ScriptableObject
{
    public EnemyType enemyType;
    public float healthScale;
    public float speedScale;
    public float damageScale;
}
public enum EnemyType
{
    Normal=0,
    Fast=1,
    Strong=2,
    Tanky=3,
    Perfect=4
}
// Try add attack speed if possible
public class UnitBaseAttribute{
    public const float health = 100f;
    public const float speed = 1f;
    public const float damage = 10f;
}
public class UnitAttriBute{
    private float health;
    private float speed;
    private float damage;
    private Faction faction;
    public UnitAttriBute(float health, float speed, float damage, Faction faction)
    {
        if(speed < 1){
            speed =1;
        }
        this.health = health;
        this.speed = speed;
        this.damage = damage;
        this.faction = faction;
    }
    public float GetHealth()
    {
        return health;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetDamage()
    {
        return damage;
    }
    public Faction GetFaction()
    {
        return faction;
    }
    public void SetFaction(Faction faction)
    {
        this.faction = faction;
    }
}