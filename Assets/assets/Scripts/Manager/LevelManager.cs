using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public List<Level> levels;

    public string levelNow;
    public int groupNow;

    public int giantCountNow;
    public int giantCountSignal;

    public int totalGroup;

    public delegate void OnLevelChange();
    public OnLevelChange OnLevelChangedCallback;

    public int rewardCurrent;

    BuffManager buffManager;
    ExpManager expManager;
    LevelManager levelManager;
    UIManager uIManager;

    public bool nextLevel;


    // Start is called before the first frame update
    void Start()
    {
        SetUpLevel();
        buffManager = BuffManager.instance;
        expManager = ExpManager.instance;
        levelManager = LevelManager.instance;
        uIManager = UIManager.instance;
    }

    void SetUpLevel() {
        foreach (Level _level in levels)
        {
            _level.giantCount = new int[_level.groupOfLevel.Length];
            for (int i = 0; i < _level.giantCount.Length; i++)
            {
                _level.giantCount[i] = i + 1;
            }
        }
    }

    public void LevelCreate() {
        LevelCheck(levelNow, groupNow);
    }

    void LevelCheck(string _levelName, int _groupLevel) {
        foreach (Level _level in levels)
        {
            if (_level.levelName == _levelName)
            {
                levelNow = _levelName;
                groupNow = _groupLevel;

                rewardCurrent = _level.reward;

                totalGroup = _level.groupOfLevel.Length;

                if (OnLevelChangedCallback != null)
                    OnLevelChangedCallback.Invoke();

                for (int i = 0; i < _groupLevel; i++)
                    _level.groupOfLevel[i] = true;
                SpawnPoints.instance.SpawnGiantNow(_level.giantCount[_groupLevel - 1]);
                giantCountNow = _level.giantCount[_groupLevel - 1];
                giantCountSignal = _level.giantCount[_groupLevel - 1];
                break;
            }
            else
                for (int i = 0; i < _level.groupOfLevel.Length; i++)
                    _level.groupOfLevel[i] = true;
        }
    }

    public void LevelUp() {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].levelName == levelNow)
            {
                if (i + 1 < levels.Count)
                {
                    levelNow = levels[i + 1].levelName;
                    groupNow = 1;

                    if (OnLevelChangedCallback != null)
                        OnLevelChangedCallback.Invoke();

                    SaveLoadDataManager.SaveData();
                    nextLevel = true;

                    expManager.SetGunExp(
                        levelManager.levels[int.Parse(levelManager.levelNow) - 1].expReward
                        + expManager.expGunCurrent);

                    UIManager.instance.ConfirmPanelOn(nextLevel);

                    break;
                }
                else Debug.Log("Win game!");

            }
        }
    }

    public void GroupUp() {
        foreach (Level _level in levels)
        {
            if (_level.levelName == levelNow)
            {
                if (groupNow < _level.groupOfLevel.Length)
                {
                    _level.groupOfLevel[groupNow] = true;
                    groupNow += 1;

                    if (OnLevelChangedCallback != null)
                        OnLevelChangedCallback.Invoke();

                    nextLevel = false;

                    expManager.SetGunExp(
                        levelManager.levels[int.Parse(levelManager.levelNow) - 1].expReward
                        + expManager.expGunCurrent);

                    UIManager.instance.ConfirmPanelOn(nextLevel);
                    
                }
                else LevelUp();
                break;
            }
        }
    }

    public void GiantMinus() {
        giantCountNow -= 1;
        if (giantCountNow <= 0)
        {
            UIManager.instance.canShoot = false;
            GroupUp();
        }
    }

    public void MinusSignal() {
        giantCountSignal -= 1;
        ChangeReward(100);
        if (giantCountSignal <= 0)
        {
            CameraAnimation.instance.scopeUI.SetActive(false);
        }
    }

    //HeadShot +100 coins
    //Enemydead +100 coins
    public void ChangeReward(int value) {
        rewardCurrent += value + (buffManager.levelBulletProfitBuff * buffManager.profitBuff);
        uIManager.ChangeCoinReward(rewardCurrent);
    }

    //GetReward for coins.
    public void GetRewards() {

        CoinManager.instance.ChangeCoin(rewardCurrent);
        SaveLoadSysterm.instance.SaveCoinData();
    }

    public int GetTotalGroupLevel() {
        foreach (Level _level in levels)
        {
            if (_level.levelName == levelNow)
            {
                return _level.groupOfLevel.Length; 
            }
        }

        return 0;
    }
}
