using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public UpgradeElement UpgradePrefab;
    public GameObject UpgradesParent;

    public void Populate(List<Upgrade> upgrades)
    {
        foreach (var upgrade in upgrades)
        {
            var upgradeElement = Instantiate(UpgradePrefab, UpgradesParent.transform);
            upgradeElement.SetUpgrade(upgrade);
        }
    }

    public void UpdateUpgrade(Upgrade upgrade)
    {
        foreach (var element in UpgradesParent.GetComponentsInChildren<UpgradeElement>())
        {
            if (element.GetContext() == upgrade)
            {
                element.SetUpgrade(upgrade);
                break;
            }
        }
    }
}
