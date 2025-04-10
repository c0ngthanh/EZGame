using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=>{
            GameManager.instance.gameState = GameState.Waiting;
            SoundManager.instance.PlayMainSceneMusic();
            SceneManager.LoadScene("MainScene");
        });
    }
}
