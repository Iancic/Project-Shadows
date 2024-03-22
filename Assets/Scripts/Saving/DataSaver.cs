using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DataSaver
{
    private static readonly string savePath = Application.persistentDataPath + "/save.json";
    public struct UpgradeData
    {
        public UpgradeType Type;
        public int Level;
    }

    public static void SaveUpgrades(List<Upgrade> upgrades)
    {
        List<UpgradeData> upgradeData = new List<UpgradeData>();
        foreach (var upgrade in upgrades)
        {
            UpgradeData data = new UpgradeData
            {
                Type = upgrade.Definition.Type,
                Level = upgrade.Level
            };
            upgradeData.Add(data);
        }

        string json = JsonConvert.SerializeObject(upgradeData);
        File.WriteAllText(savePath, json);
    }
    
    public static List<UpgradeData> LoadUpgrades()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonConvert.DeserializeObject<List<UpgradeData>>(json);
        }
        return new List<UpgradeData>();
    }
}
