using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeElement : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Value;
    public TextMeshProUGUI Cost;
    public Button BuyButton;
    
    private Upgrade _upgrade;
    
    public Upgrade GetContext() => _upgrade;

    private void Start()
    {
    }
    
    public void SetUpgrade(Upgrade upgrade)
    {
        _upgrade = upgrade;
        Name.text = upgrade.Definition.UpgradeName;
        Value.text = "Value: " + upgrade.GetValue();
        Cost.text = "Cost: " + upgrade.Cost;
        
        BuyButton.interactable = StatsManager.Instance.GetMoney() >= upgrade.Cost && upgrade.Level < upgrade.Definition.MaxLevel;
        
        BindCallback();
    }

    private void BindCallback()
    {
        BuyButton.onClick.RemoveAllListeners();
        BuyButton.onClick.AddListener(OnBuyClick);
    }

    private void OnBuyClick()
    {
        UpgradesManager.Instance.BuyUpgrade(_upgrade);
    }
}