using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public Upgrade(UpgradeDefinition definition)
    {
        Definition = definition;
        Level = 0;
        Cost = (int)definition.BaseCost;
    }
    
    public UpgradeDefinition Definition;
    public int Level;
    public int Cost;

    public void UpgradeLevel()
    {
        if (Level < Definition.MaxLevel && StatsManager.Instance.GetMoney() >= Cost)
        {
            Level++;
            Cost = (int)(Definition.BaseCost + (Definition.CostIncrease * Level));
        }
    }

    public float GetValue()
    {
        return Definition.BaseValue + (Definition.ValueStep * Level);
    }
}
