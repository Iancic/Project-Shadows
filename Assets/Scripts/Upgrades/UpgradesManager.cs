using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance;
    
    public List<UpgradeDefinition> UpgradeDefinitions;
    public Shop ShopUI;
    
    public Action<UpgradeType, float> OnUpgradeChanged;

    private List<Upgrade> _upgrades;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (_upgrades == null)
        {
            LoadUpgrades();
        }
    }

    private void Update()
    {
    }

    public void LoadUpgrades()
    {
        // TODO: Load upgrades from save file
        List<DataSaver.UpgradeData> loadedUpgrades = DataSaver.LoadUpgrades();
        _upgrades = new List<Upgrade>();

        foreach (var loaded in loadedUpgrades)
        {
            var upgradeDefinition = UpgradeDefinitions.Find(u => u.Type == loaded.Type);
            if (upgradeDefinition == null)
            {
                throw new Exception("The upgrade definition you are searching for does not exist.");
            }
            
            var upgrade = new Upgrade(upgradeDefinition);
            upgrade.Level = loaded.Level;
            _upgrades.Add(upgrade);
        }
        
        // If there are new upgrades, add them to the list
        foreach (var definition in UpgradeDefinitions)
        {
            if (_upgrades.Find(u => u.Definition.Type == definition.Type) == null)
            {
                _upgrades.Add(new Upgrade(definition));
            }
        }

        if (ShopUI != null)
        {
            ShopUI.Populate(_upgrades);
        }
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        if (StatsManager.Instance.GetMoney() >= upgrade.Cost && upgrade.Level < upgrade.Definition.MaxLevel)
        {
            StatsManager.Instance.RemoveMoney(upgrade.Cost);
            upgrade.UpgradeLevel();
            OnUpgradeChanged?.Invoke(upgrade.Definition.Type, upgrade.GetValue());
            if (ShopUI != null)
            {
                ShopUI.UpdateUpgrade(upgrade);
            }
        }
    }

    public float GetValue(UpgradeType type)
    {
        if (_upgrades == null)
        {
            LoadUpgrades();
        }
        
        var upgrade = _upgrades.Find(u => u.Definition.Type == type);
        if (upgrade == null)
        {
            throw new Exception("The upgrade value you are searching for does not exist.");
        }

        return upgrade.GetValue();
    }

    private void OnApplicationQuit()
    {
        DataSaver.SaveUpgrades(_upgrades);
    }
}