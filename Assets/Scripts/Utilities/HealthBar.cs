using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthBarVisual;
    [SerializeField] private Unit unit;
    private float timer = 0;
    private float timerMax = 0.2f;
    void Awake()
    {
        unit.onUnitAttacked += UpdateHealthBar;
        gameObject.SetActive(false);
    }
    private void UpdateHealthBar(object sender, float e)
    {
        if(gameObject.activeSelf == false){
            gameObject.SetActive(true);
        }
        if(e == 1){
            gameObject.SetActive(false);
        }
        healthBarVisual.transform.localScale = new Vector3(e, healthBarVisual.transform.localScale.y, healthBarVisual.transform.localScale.z);
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0){
            timer = timerMax;
            if (Camera.main != null)
            {
                transform.LookAt(Camera.main.transform);
                transform.Rotate(0, 180, 0); // Flip the health bar to face the camera correctly
            }
        }
    }
}
