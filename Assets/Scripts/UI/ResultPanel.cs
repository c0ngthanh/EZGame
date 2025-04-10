using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private Text resultText;
    [SerializeField] private Button mainMenuBtn;
    void Start()
    {
        mainMenuBtn.onClick.AddListener(() => {
            SoundManager.instance.PlayMainSceneMusic();
            GameManager.instance.GoToMainMenu();
        });
        GameManager.instance.OnResultEvent += ShowResult;
        gameObject.SetActive(false);
    }

    private void ShowResult(object sender, bool e)
    {
        if(e){
            resultText.text = "You Win!";
        }else{
            resultText.text = "You Lose!";
        }
        gameObject.SetActive(true);
        GameManager.instance.OnResultEvent -= ShowResult;
    }
}
