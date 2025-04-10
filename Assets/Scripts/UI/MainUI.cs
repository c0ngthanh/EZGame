using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private ModifierText gameModeText;
    [SerializeField] private ModifierText levelText;
    [SerializeField] private ModifierText enemyTypeText;
    [SerializeField] private ModifierText playerTypeText;
    [SerializeField] private BaseInfo enemyInfo;
    [SerializeField] private BaseInfo playerInfo;
    [SerializeField] private Button goBtn;
    [SerializeField] private Button _50UnitBtn;
    void Awake()
    {
        goBtn.onClick.AddListener(() => {
            GameManager.instance.SetData(gameModeText.GetCurrentIndex(), levelText.GetCurrentIndex(), playerTypeText.GetCurrentIndex(),
            LevelManager.instance.GetEnemyCount(gameModeText.GetCurrentIndex(), levelText.GetCurrentIndex()),gameModeText.GetCurrentIndex() == 2 ? 2 : 0);
            GameManager.instance.StartGame();
            SoundManager.instance.PlayInGameMusic();
        });
        _50UnitBtn.onClick.AddListener(() => {
            GameManager.instance.StartTesting();
            SoundManager.instance.PlayInGameMusic();
        });
    }
    void Start()
    {
        gameModeText.SetStrings(Enum.GetNames(typeof(GameMode)));
        string[] level = new string[LevelManager.instance.gamemodeList[0].levelPercent.Length];
        for (int i = 0; i < level.Length; i++)
        {
            level[i] = i.ToString();
        }
        levelText.SetStrings(level);
        enemyTypeText.SetStrings(Enum.GetNames(typeof(EnemyType)));
        playerTypeText.SetStrings(Enum.GetNames(typeof(EnemyType)));
        ShowPreviewInfo();
    }
    public void ShowPreviewInfo(){
        enemyInfo.Show(LevelManager.instance.GetUnitData(gameModeText.GetCurrentIndex(), levelText.GetCurrentIndex(), enemyTypeText.GetCurrentIndex())
        , LevelManager.instance.GetEnemyCount(gameModeText.GetCurrentIndex(), levelText.GetCurrentIndex()));
        playerInfo.Show(LevelManager.instance.GetPlayerData(playerTypeText.GetCurrentIndex()), gameModeText.GetCurrentIndex() == 2 ? 2 : 0);
    }
}
