using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class LivesCounterUI : MonoBehaviour
{
    private Text livestext;
    void Awake()
    {
        livestext = GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        livestext.text = "Lives: "  +  GameMaster.RemainningLives.ToString();
    }
}
