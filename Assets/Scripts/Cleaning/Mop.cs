using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : MonoBehaviour
{
    public Transform MopTip;
    public int Efficiency = 1;

    private List<CleanupItem> _allItems = new List<CleanupItem>();
    private List<Material> _allMaterials = new List<Material>();

    private void Start()
    {
        CleanupItem[] items = FindObjectsOfType<CleanupItem>();
        foreach (CleanupItem item in items)
        {
            _allItems.Add(item);
            item.InjectMopRef(this);
            _allMaterials.Add(item.GetComponent<Renderer>().material);
        }
    }

    private void Update()
    {
        // _MopPos in all shaders
        // BloodShaderHandler.Instance.UpdateData(MopTip.position);
        // Vector4 mopPos = MopTip.position;
        // foreach (Material material in _allMaterials)
        // {
        //     material.SetVector("_MopPos", mopPos);
        // }
    }
    
    public Vector3 GetMopTipPosition()
    {
        return MopTip.position;
    }
}
