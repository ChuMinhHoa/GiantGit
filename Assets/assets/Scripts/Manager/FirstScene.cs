using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveLoadSysterm saveLoadSysterm = SaveLoadSysterm.instance;
        SceneChangeManager sceneChangeManager = SceneChangeManager.instance;

        saveLoadSysterm.SaveData();
        saveLoadSysterm.SaveBuffData();
        saveLoadSysterm.SaveCoinData();
        saveLoadSysterm.SaveExpData();

        saveLoadSysterm.GetData();
        saveLoadSysterm.LoadData();

        saveLoadSysterm.GetBuffData();
        saveLoadSysterm.LoadBuffData();

        saveLoadSysterm.GetCoinData();
        saveLoadSysterm.LoadCoinData();

        saveLoadSysterm.GetExpData();
        saveLoadSysterm.LoadExpData();

        if (saveLoadSysterm.data != null)
        {
            sceneChangeManager.ChangeScene(int.Parse(saveLoadSysterm.data.level));
        }
    }

}
