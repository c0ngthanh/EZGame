using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private ModifierText gameModeText;
    [SerializeField] private ModifierText levelText;
    [SerializeField] private ModifierText enemyTypeText;
    [SerializeField] private ModifierText playerTypeText;
    void Start()
    {
        gameModeText.SetStrings(Enum.GetNames(typeof(GameMode)));
        string[] level = new string[LevelManager.instance.levels.Length];
        for (int i = 0; i < LevelManager.instance.levels.Length; i++)
        {
            level[i] = i.ToString();
        }
        levelText.SetStrings(level);
        enemyTypeText.SetStrings(Enum.GetNames(typeof(EnemyType)));
        playerTypeText.SetStrings(Enum.GetNames(typeof(EnemyType)));
    }
}
