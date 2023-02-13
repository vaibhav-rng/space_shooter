using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthbar;
    [SerializeField]
    private Text Healthtext;

    void Start()
    {
        if (healthbar == null)
        {
            Debug.LogError("STATUS INDICATOR: No health bar object referenced!");
        }
        if (Healthtext == null)
        {
            Debug.LogError("STATUS INDICATOR: No health text object referenced!");
        }
    }

    public void SetHealth(int _currentHP,int _maxHP)
    {
        float _value = (float)_currentHP / _maxHP;
        healthbar.localScale = new Vector3(_value, healthbar.localScale.y, healthbar.localScale.z);
        Healthtext.text = _currentHP + "/" + _maxHP;
    }
}
