using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance;
    
    public List<UpgradeDefinition> UpgradeDefinitions;
    public Shop ShopUI;
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
        // TODO: Load upgrades from save file
        _upgrades = new List<Upgrade>();
        foreach (var upgradeDefinition in UpgradeDefinitions)
        {
            var upgrade = new Upgrade(upgradeDefinition);
            _upgrades.Add(upgrade);
        }

        if (ShopUI != null)
        {
            ShopUI.Populate(_upgrades);
        }
    }

    private void Update()
    {
        Debug.Log($"Value for upgrade type: {UpgradeType.MovementSpeed} is {GetValue(UpgradeType.MovementSpeed)}");
        Debug.Log($"Value for upgrade type: {UpgradeType.ReloadTime} is {GetValue(UpgradeType.ReloadTime)}");
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        if (StatsManager.Instance.GetMoney() >= upgrade.Cost && upgrade.Level < upgrade.Definition.MaxLevel)
        {
            StatsManager.Instance.RemoveMoney(upgrade.Cost);
            upgrade.UpgradeLevel();
            if (ShopUI != null)
            {
                ShopUI.UpdateUpgrade(upgrade);
            }
        }
    }

    public float GetValue(UpgradeType type)
    {
        var upgrade = _upgrades.Find(u => u.Definition.Type == type);
        if (upgrade == null)
        {
            throw new Exception("The upgrade value you are searching for does not exist.");
        }

        return upgrade.GetValue();
    }
}