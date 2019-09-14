using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class MovingLogic : MonoBehaviour
{
    public static MovingLogic instance;

    [SerializeField]
    private GameObject ScoreBoard;

    [SerializeField]
    private List<DificultyData> dificultyDatas = new List<DificultyData>();

    public Grid grid;

    private CrossOrCircle nowFigureMoving = CrossOrCircle.Cross;

    private TMP_Text _text;
    public TMP_Text Text
    {
        get
        {
            if (_text != null)
            {
                return _text;
            }
            else
            {
                if (ScoreBoard != null)
                {
                    return _text = ScoreBoard.GetComponent<TMP_Text>();
                }
                else
                {
                    throw new System.Exception("There is no 'Scoreboard'? Attach it to this GO in inspector");
                }
            }
        }
    }

    private AbstractAI _ai;
    private AbstractAI Ai
    {
        get
        {
            return _ai ?? (_ai = dificultyDatas.Find(x => x.dificulty == GameManager.instance.currentDificulty).aI);
        }
    }

    private void Awake()
    {
        #region Singleton
        //Singleton only for 'GameScene'
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        #endregion
        grid = FindObjectOfType<Grid>();
        //RemoveDifficyltyDuples();
    }

    /*private void RemoveDifficyltyDuples()
    {
            dificultyDatas.ForEach(x =>
            {
                bool isOneFound = false; 
                dificultyDatas.ForEach(y =>
                {
                    if (x.dificulty == y.dificulty)
                    {
                        if (isOneFound)
                        {
                            dificultyDatas.Remove(y);
                        }
                        else
                        {
                            isOneFound = true;
                        }
                    }
                });
            });
    }*/

    private void Update()
    {
        Text.text = $"Score\nWins:\t{GameManager.instance.myScoreCur}\nLoses:\t{GameManager.instance.enemyScoreCur}\nDraws:\t{GameManager.instance.draws}";
    }

    //Player's move
    public void PlayerMove(GameObject button)
    {
        bool isFree = button.GetComponent<Cell>().innerValue == null;
        if (GameManager.instance.curPlayer != Player.User || !isFree) { return; }

        button.GetComponent<Cell>().SpawnFigure(nowFigureMoving);

        if (!IsThereAWinner())
        {
            //Swap moving figure
            nowFigureMoving = (nowFigureMoving == CrossOrCircle.Cross) ? CrossOrCircle.Circle : CrossOrCircle.Cross;

            //Swap movin player
            GameManager.instance.curPlayer = Player.Computer;

            MakeComputerMove();
        }
    }

    //Computer's move
    private void MakeComputerMove()
    {
        Ai.CalculateMoveCell().SpawnFigure(nowFigureMoving);

        if (!IsThereAWinner())
        {
            //Swap moving figure
            nowFigureMoving = (nowFigureMoving == CrossOrCircle.Cross) ? CrossOrCircle.Circle : CrossOrCircle.Cross;

            //Swap movin player
            GameManager.instance.curPlayer = Player.User;
        }
    }

    private bool IsThereAWinner()
    {
        if (grid.CheckForWinner())
        {
            Debug.Log("Winner is " + GameManager.instance.curPlayer.ToString());
            return true;
        }
        return false;
    }

    //private IEnumerator EndGame(bool hasWinner)
    //{
    //    yield return null;
    //}


    [System.Serializable]
    private struct DificultyData
    {
        public Dificulty dificulty;
        public AbstractAI aI;
        
    }
}
