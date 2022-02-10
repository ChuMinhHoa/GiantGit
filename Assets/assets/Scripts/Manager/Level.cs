using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string levelName;
    public bool[] groupOfLevel;
    public int[] giantCount;

    public int reward;
    public int expReward=200;
}
