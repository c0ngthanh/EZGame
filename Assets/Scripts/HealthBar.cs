using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthBarVisual;
    [SerializeField] private Unit unit;
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
        healthBarVisual.transform.localScale = new Vector3(e, healthBarVisual.transform.localScale.y, healthBarVisual.transform.localScale.z);
    }
}
