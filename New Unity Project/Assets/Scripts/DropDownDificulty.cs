using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public enum Dificulty
{
    ez, DragonAge, ChuckNorris
}
public class DropDownDificulty : MonoBehaviour
{
    public Dificulty dificulty;
    private TMP_Dropdown dropdown;
    private void Awake()
    {
        dropdown = FindObjectOfType<TMP_Dropdown>();
        SetUp();
    }
    private void SetUp()
    {
        string[] dificultyNames = Enum.GetNames(typeof(Dificulty));
        List<string> names = new List<string>(dificultyNames);
        dropdown.AddOptions(names);
    }
    private Dificulty SetDificulty()
    {
        int value = dropdown.value;
        switch (value)
        {
            case 0:
                { return dificulty = Dificulty.ez; }
            case 1:
                { return dificulty = Dificulty.DragonAge; }
            case 2:
                { return dificulty = Dificulty.ChuckNorris; }
            default: {return dificulty = Dificulty.ez; }
        }
    }
    private void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SetDificulty();
            GameManager.instance.setDificultyEvent.Invoke();
            Debug.Log(dropdown.value);
        }
    }
}
