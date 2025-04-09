using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInfo : MonoBehaviour
{
    public Text healthText;
    public Text speedText;
    public Text damageText;
    public Text countText;
    public void Show(UnitAttriBute unitAttriBute, int count)
    {
        countText.text = count.ToString("F0");
        healthText.text = unitAttriBute.GetHealth().ToString("F0");
        speedText.text = unitAttriBute.GetSpeed().ToString("F0");
        damageText.text = unitAttriBute.GetDamage().ToString("F0");
    }
}
