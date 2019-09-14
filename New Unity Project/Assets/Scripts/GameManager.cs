using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomValueStructs;

public enum Player
{
    User, Computer
}
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent setDificultyEvent = new UnityEvent();
    public static GameManager instance;

    public GameObject crossPrefab;
    public GameObject circlePrefab;
    private DropDownDificulty dropDownDificulty;
    public int myScoreCur = 0;
    public int enemyScoreCur = 0;
    public int draws = 0;

    public TwoInts GameFieldSize;
    public Dificulty currentDificulty;
    public Player curPlayer;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        dropDownDificulty = FindObjectOfType<DropDownDificulty>();
        setDificultyEvent.AddListener(()=> 
        {
            SetDificulty();
            SetUser();
        });
    }

    public void Increment(int myScoreCur, int enemyScoreCur, int draws)
    {
        this.myScoreCur += myScoreCur;
        this.enemyScoreCur += enemyScoreCur;
        this.draws += draws;
    }
    void SetDificulty()
    {
        currentDificulty = dropDownDificulty.dificulty;
    }

    void SetUser()
    {
        if (currentDificulty != Dificulty.ChuckNorris)
        {
            curPlayer = Player.User;
        }
        else
        {
            curPlayer = Player.Computer;
        }
    }
    private void OnDestroy()
    {
        setDificultyEvent.RemoveListener(() =>
        {
            SetDificulty();
            SetUser();
        });
    }
}
