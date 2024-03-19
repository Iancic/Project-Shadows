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
        BindCallback();
    }

    private void BindCallback()
    {
        BuyButton.onClick.RemoveAllListeners();
        BuyButton.onClick.AddListener(OnBuyClick);
    }

    private void Update()
    {
        if (_upgrade == null) return;
        
        BuyButton.interactable = StatsManager.Instance.GetMoney() >= _upgrade.Cost && _upgrade.Level < _upgrade.Definition.MaxLevel;
    }

    private void OnBuyClick()
    {
        Debug.Log("Clicked upgrade");
        UpgradesManager.Instance.BuyUpgrade(_upgrade);
    }
}