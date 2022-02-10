using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadDataManager
{

    public static void SaveData() {

        string path = Application.persistentDataPath + "/Gamedata.data";

        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
            File.Delete(path);

        FileStream stream = new FileStream(path, FileMode.Create);

        LevelManager levelManager = LevelManager.instance;

        SaveData newSaveData = new SaveData(levelManager);

        formatter.Serialize(stream, newSaveData);

        stream.Close();

    }

    public static SaveData LoadData() {
        string path = Application.persistentDataPath + "/Gamedata.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData savedData = formatter.Deserialize(stream) as SaveData;

            stream.Close();

            return savedData;
        }
        else
        {
            Debug.Log("File not Exits!");

            return null;
        }
    }

    public static void SaveBuffData() {
        string path = Application.persistentDataPath + "/BuffData.data";
        if (File.Exists(path))
            File.Delete(path);

        FileStream stream = new FileStream(path, FileMode.Create);

        BinaryFormatter formatter = new BinaryFormatter();

        BuffManager buffManager = BuffManager.instance;

        BuffData newBuffData = new BuffData(buffManager);

        formatter.Serialize(stream, newBuffData);

        stream.Close();
    }

    public static BuffData LoadBuffData()
    {

        string path = Application.persistentDataPath + "/BuffData.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            BuffData buffData = formatter.Deserialize(stream) as BuffData;

            stream.Close();

            return buffData;
        }
        else {
            Debug.Log("File doesn't exists.");
            return null;
        }

    }

    public static void SaveCoinData() {
        string path = Application.persistentDataPath + "/CoinsData.data";
        if (File.Exists(path))
            File.Delete(path);

        FileStream stream = new FileStream(path, FileMode.Create);

        BinaryFormatter formatter = new BinaryFormatter();

        CoinManager coinManager = CoinManager.instance;
        CoinData coinData = new CoinData(coinManager.GetCoins());

        formatter.Serialize(stream, coinData);

        stream.Close();
    }

    public static CoinData LoadCoinData() {
        string path = Application.persistentDataPath + "/CoinsData.data";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            CoinData coinData = formatter.Deserialize(stream) as CoinData;

            stream.Close();

            return coinData;
        }
        else {
            Debug.Log("File doesn't exisits.");
            return null;
        }
    }

    public static void SaveExp() {
        string path = Application.persistentDataPath + "/Exp.data";

        if (File.Exists(path))
            File.Delete(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        ExpManager expManager = ExpManager.instance;
        GunAndBulletManager gunAndBulletManager = GunAndBulletManager.instance;

        ExpData expData = new ExpData(expManager, gunAndBulletManager);
        

        binaryFormatter.Serialize(stream, expData);

        stream.Close();
    }

    public static ExpData LoadExp() {
        string path = Application.persistentDataPath + "/Exp.data";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ExpData expData = binaryFormatter.Deserialize(stream) as ExpData;
            stream.Close();
            return expData;
        }
        else {
            Debug.Log("File doesn't Exists.");
            return null;
        }
    }
}
