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
            fill.color = color;

            mask.maxValue = levelManager.totalGroup;
            levelCurrent = levelManager.levelNow;

            mask.value = levelManager.groupNow;
            textProgress.text = "Level " +
                levelCurrent + "-" +
                levelManager.groupNow.ToString() + "/" +
                mask.maxValue.ToString();

        }
        

    }


    bool GameobjCheck() {
        if (fill == null)
            return false;
        return true;
    }
}
