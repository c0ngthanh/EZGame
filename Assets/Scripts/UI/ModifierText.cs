using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierText : MonoBehaviour
{
    public Button left;
    public Button right;
    public MainUI mainUI;
    private Text _text;
    private string[] strings;
    private int currentIndex;
    private void Awake()
    {
        left.onClick.AddListener(() => ShowString(-1));
        right.onClick.AddListener(() => ShowString(1));
        _text = GetComponent<Text>();
    }
    private void ShowString(int value)
    {
        currentIndex = (currentIndex + value+strings.Length) % strings.Length;
        mainUI.ShowPreviewInfo();
        _text.text = strings[currentIndex];
    }
    public void SetStrings(string[] strings)
    {
        if(strings.Length == 0){
            Debug.LogError("ModifierText: strings is empty!");
            return;   
        }
        this.strings = strings;
        currentIndex = 0;
        _text.text = strings[currentIndex];
    }
    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}