using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string level;
    public int groupNow;

    public SaveData(LevelManager levelManager) {

        level = levelManager.levelNow;
        groupNow = levelManager.groupNow;


    }
}
