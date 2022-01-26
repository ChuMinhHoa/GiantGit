using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProgressBar : MonoBehaviour
{

    public int groupCurrent;
    public string levelCurrent;

    public Slider mask;
    public Image fill;

    public Color color;

    public TextMeshProUGUI textProgress;

    LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.instance;
        levelManager.OnLevelChangedCallback += UpdateCurrentFill;
    }

    void UpdateCurrentFill() {
        if (GameobjCheck())
        {
            groupCurrent = levelManager.groupNow;
            mask.maxValue = levelManager.totalGroup;

            mask.value = groupCurrent;

            levelCurrent = levelManager.levelNow;

            fill.color = color;

            textProgress.text = "Level " + 
                levelCurrent + "-" + 
                mask.value.ToString() + "/" + 
                mask.maxValue.ToString();
        }
        

    }


    bool GameobjCheck() {
        if (fill == null)
            return false;
        return true;
    }
}
