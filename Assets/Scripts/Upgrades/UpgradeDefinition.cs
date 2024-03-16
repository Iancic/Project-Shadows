using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeDefinition", menuName = "Upgrades/UpgradeDefinition")]
public class UpgradeDefinition : ScriptableObject
{
    public string UpgradeName;
    public UpgradeType Type;
    
    // Cost calculations
    public float BaseCost;
    public float CostIncrease;
    
    // Upgrade values
    public float BaseValue;
    public float ValueStep;

    public int MaxLevel;
}
