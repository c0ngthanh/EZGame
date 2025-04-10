using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSShow : MonoBehaviour
{
    private Text fpsText;
    private float timer = 0;
    // Update is called once per frame
    void Start()
    {
        fpsText = GetComponent<Text>();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <0)
        {
            timer = 0.2f;
            fpsText.text = "FPS: " + (int)(1f / Time.unscaledDeltaTime);
        }
    }
}
