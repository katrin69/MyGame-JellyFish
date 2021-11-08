using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelBar : MonoBehaviour
{
    public Text levelText; //показывает лэвл

    private void Start()
    {
        if (levelText != null)
        {
            levelText.text = "";
        }
    }
    public void SetValue(float level)
    {
        levelText.text = "" + level;
    }

}
