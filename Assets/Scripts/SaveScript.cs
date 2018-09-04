using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public class SaveScript {

    const string expectedSaveVer = "0.01";

    static int totalWorlds=3;    

    public List<LevelInfo> currentLevels;
    public SaveInfo saveInfo;

    private static string savePath = Application.persistentDataPath + "/koizoSave.dat";
    

    public SaveScript()
    {
        LoadSave();

        if (saveInfo.levelInfos == null)
        {
            saveInfo.levelInfos = new List<LevelInfo>();

            saveInfo.levelInfos.Add(new LevelInfo(1, 1, startsAvailable: true));
            saveInfo.levelInfos.Add(new LevelInfo(1, 2));
            saveInfo.levelInfos.Add(new LevelInfo(1, 3));
            saveInfo.levelInfos.Add(new LevelInfo(1, 4));
            saveInfo.levelInfos.Add(new LevelInfo(1, 5));
            saveInfo.levelInfos.Add(new LevelInfo(1, 6));
            saveInfo.levelInfos.Add(new LevelInfo(1, 7));
            saveInfo.levelInfos.Add(new LevelInfo(2, 1));
            saveInfo.levelInfos.Add(new LevelInfo(2, 2));
            saveInfo.levelInfos.Add(new LevelInfo(2, 3));
            saveInfo.levelInfos.Add(new LevelInfo(2, 4));
            saveInfo.levelInfos.Add(new LevelInfo(2, 5));        
            saveInfo.levelInfos.Add(new LevelInfo(2, 6));
            saveInfo.levelInfos.Add(new LevelInfo(2, 7));
            saveInfo.levelInfos.Add(new LevelInfo(3, 1));
            saveInfo.levelInfos.Add(new LevelInfo(3, 2));
            saveInfo.levelInfos.Add(new LevelInfo(3, 3));
            saveInfo.levelInfos.Add(new LevelInfo(3, 4));
            saveInfo.levelInfos.Add(new LevelInfo(3, 5));
            saveInfo.levelInfos.Add(new LevelInfo(3, 6));
            saveInfo.levelInfos.Add(new LevelInfo(3, 7));

            saveInfo.ver = expectedSaveVer;
        }

        currentLevels = saveInfo.levelInfos;

    }

    public void LoadSave()
    {
        Debug.Log(savePath);

        if(File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);

            SaveInfo info = (SaveInfo)bf.Deserialize(file);

            saveInfo = info;

            file.Close();

            if(saveInfo.ver != expectedSaveVer)
            {
                UpdateSaveVer(saveInfo);
            }

        }
        else
        {
            Debug.Log("Save file not found. Creating new one");
            CreateNewSave();
        }

    }

    public void CreateNewSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);

        SaveInfo data = new SaveInfo(currentLevels);

        data.gates = new List<WorldGate>();

        for (int i = 0; i < totalWorlds+1; i++)
        {
            data.gates.Add(new WorldGate());
        }

        data.gates[0].opened = true;

        bf.Serialize(file, data);
        file.Close();

        saveInfo = data;

    }

    public void UpdateSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);

        bf.Serialize(file, saveInfo);

        file.Close();

    }

    public void UpdateSaveVer(SaveInfo saveInfo)
    {
        //TODO
            //Verificar quais níveis existem e quais eu quero e adicionar os que não tem
    }

}

[System.Serializable]
public class SaveInfo
{
    public List<LevelInfo> levelInfos;
    public List<WorldGate> gates;

    public string ver;

    public SaveInfo(List<LevelInfo> levels)
    {
        levelInfos = levels;
    }

    public SaveInfo(List<LevelInfo> levels, List<WorldGate> worldGates)
    {
        levelInfos = levels;
        gates = worldGates;
    }


    public LevelInfo GetLevel(string levelName)
    {
        foreach(LevelInfo l in levelInfos)
        {
            if(l.name == levelName)
            {
                return l;
            }
        }

        Debug.Log("Failed to find " + levelName);
        return null;
    }

    public LevelInfo GetLevel(int world, int level)
    {
        foreach(LevelInfo l in levelInfos){
            if(world == l.world && level == l.level)
            {
                return l;
            }
        }
        return null;
    }

    public bool CheckLevelCompletion(int world, int level)
    {
        if (GetLevel(world, level) != null)
        {
            return GetLevel(world, level).completed;
        }

        Debug.Log("Failed to find Level" + world.ToString() + "_" + level.ToString());
        return false;
    }

    public bool CheckLevelCompletion(string levelName)
    {
        if (GetLevel(levelName) != null)
        {
            return GetLevel(levelName).completed;
        }
        Debug.Log("Failed to find " + levelName);
        return false;
    }

    public bool CheckWorldCompletion(int world)
    {
        foreach(LevelInfo l in levelInfos)
        {
            if(l.world == world)
            {
                if(l.completed == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

   

}

[System.Serializable]
public class LevelInfo
{
    public readonly int world;
    public readonly int level;

    public readonly string name;

    public bool available;
    public bool completed;


    public bool[] bonusChalanges;

    public LevelInfo(int worldNumber, int levelNumber, int bonus = 0, bool startsAvailable = false)
    {

        this.world = worldNumber;
        this.level = levelNumber;

        this.name = "Level" + world.ToString() + "_" + level.ToString();

        this.completed = false;
        this.available = startsAvailable;

        bonusChalanges = new bool[bonus];

    }

    public override string ToString()
    {
        return name;
    }


}
