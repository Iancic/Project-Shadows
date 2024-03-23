using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ClassManagement : MonoBehaviour
{
    public GameObject[] ClassesPrefab; //index 0 Freezer, index 1 Stunner, index 2 Attacker
    public GameObject[] ClassesUI;

    [HideInInspector] public static int SelectedClass;
    public static ClassManagement Instance { get; private set; }

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "CharacterMenu")
            SelectedClass = 0;
        if (SceneManager.GetActiveScene().name == "Arena")
            SpawnClass();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ForwardSelect()
    {
        if (SelectedClass + 1 < ClassesUI.Length)
        SelectedClass += 1;

        for (int i = 0; i < ClassesUI.Length; i++)
        {
            if (i == SelectedClass)
            {
                ClassesUI[i].SetActive(true);
            }
            else
            {
                ClassesUI[i].SetActive(false);
            }

        }

    }

    public void BackwardSelect() 
    {

        if (SelectedClass - 1 >= 0)
        SelectedClass -= 1;

        for (int i = 0; i < ClassesUI.Length; i++)
        {
            if (i == SelectedClass)
            {
                ClassesUI[i].SetActive(true);
            }
            else
            {
                ClassesUI[i].SetActive(false);
            }

        }

    }

    public void SpawnClass()
    {

        if (SelectedClass >= 0 && SelectedClass < ClassesPrefab.Length)
        {
            Instantiate(ClassesPrefab[SelectedClass], new Vector3(2f, 0f, 10f), Quaternion.identity);
        }
        else
        {
            throw new Exception("Class index does not exist");
        }
    }
}
