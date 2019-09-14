using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomValueStructs;
public class Grid : MonoBehaviour
{
    public GameObject grid;
    public GameObject buttonPrefab;

    private Cell[,] GameFieldCells;
    public List<Cell> buttons
    {
        get
        {
            List<Cell> retList = new List<Cell>();
            for (int i = 0; i < GameFieldSize.x; i++)
            {
                for (int j = 0; j < GameFieldSize.y; j++)
                {
                    retList.Add(GameFieldCells[i, j]);
                }
            }
            return retList;
        }
    }
    private TwoInts GameFieldSize;
    // Start is called before the first frame update
    void Start()
    {
        GameFieldSize = GameManager.instance.GameFieldSize;
        CheckForErrors();
        if (GameFieldCells == null)
        {
            GameFieldCells = new Cell[GameFieldSize.x, GameFieldSize.y];
        }
        BuildGameField();
    }

    private void CheckForErrors()
    {
        if (GameFieldSize.x < 0 || GameFieldSize.y < 0)
        {
            GameFieldSize.x = Mathf.Abs(GameFieldSize.x);
            GameFieldSize.y = Mathf.Abs(GameFieldSize.y);
            GameManager.instance.GameFieldSize = GameFieldSize;
        }
        if (GameFieldSize.x == 0 || GameFieldSize.y == 0)
        {
            Debug.LogWarning("Game field size can't be equal to null. Set it to default values (3x3)");
            GameFieldSize.x = 3;
            GameFieldSize.y = 3;
            GameManager.instance.GameFieldSize = GameFieldSize;
        }
        if (GameFieldSize.x != GameFieldSize.y)
        {
            Debug.LogWarning("Game field axes sizes can't be different. Set it to default values (3x3)");
            GameFieldSize.x = 3;
            GameFieldSize.y = 3;
            GameManager.instance.GameFieldSize = GameFieldSize;
        }
    }
    public void BuildGameField()
    {
        for (int i = 0; i < GameFieldSize.x; i++)
        {
            for (int j = 0; j < GameFieldSize.y; j++)
            {
                GameObject newbutton = Instantiate(buttonPrefab, transform.position, Quaternion.identity);
                newbutton.transform.localScale *= 0.3f;
                newbutton.transform.SetParent(grid.transform);
                GameFieldCells[i, j] = newbutton.GetComponent<Cell>();
            }
        }
    }

    public void Reset()
    {
        for (int i = 0; i < GameFieldSize.x; i++)
        {
            for (int j = 0; j < GameFieldSize.y; j++)
            {
                if (GameFieldCells[i, j].CrossOrCircleGO == null)
                {
                    continue;
                }
                //if (cell.GetComponentInChildren<GameObject>() == null)
                MovingLogic.instance.EndGameBoard.transform.localScale = Vector2.zero;
                Destroy(GameFieldCells[i, j].CrossOrCircleGO);
                GameFieldCells[i, j].button.transform.GetComponent<Button>().interactable = true;
                GameFieldCells[i, j].button.transform.GetComponent<Cell>().innerValue = null;
                //GameManager.instance.curPlayer = (GameManager.instance.curPlayer == Player.Computer ? Player.User : Player.Computer);
            }
        }
                if (GameManager.instance.curPlayer == Player.Computer)
                {
                    MovingLogic.instance.InvokeComputerMove();
                }
    }

    //public bool CheckForWinner()
    //{
    //    for (int i = 0; i < 6; i += 3)
    //    {
    //        if (!CheckValues(i, i + 1))
    //            continue;
    //        if (!CheckValues(i, i + 2))
    //            continue;
    //        //return true;
    //    }

    //    for (int i = 0; i < 2; i++)
    //    {
    //        if (!CheckValues(i, i + 3))
    //            continue;
    //        if (CheckValues(i, i + 6))
    //            continue;
    //        //return true;
    //    }

    //    if (CheckValues(0, 4) && CheckValues(0, 8))
    //        return true;

    //    if (CheckValues(2, 4) && CheckValues(2, 6))
    //        return true;
    //    return false;
    //}

    public bool CheckForWinner()
    {
        for (int i = 0; i < GameFieldSize.x; i++)
        {
            if (GameFieldCells[i, 0].innerValue != null)
            {
                // Check horizontal lines
                for (int j = 0; j < GameFieldSize.y; j++)
                {
                    if (GameFieldCells[i, 0].innerValue != GameFieldCells[i, j].innerValue)
                    {
                        break;//throw out the loop
                    }
                    if (j == GameFieldSize.y - 1)
                    {
                        return true;
                    }
                }

                //Check for 'upper-left'-'lower-right' diagonal 
                if (i == 0)
                {
                    for (int j = 0; j < GameFieldSize.x; j++)
                    {
                        if (GameFieldCells[i, 0].innerValue != GameFieldCells[(i + j), j].innerValue)
                        {
                            break;
                        }
                        if (j == GameFieldSize.x - 1)
                        {
                            return true;
                        }
                    }
                }

                //Check for 'Lower-left'-'upper-right' diagonal 
                if (i == GameFieldSize.x - 1)
                {
                    for (int j = 0; j < GameFieldSize.x; j++)
                    {
                        if (GameFieldCells[i, 0].innerValue != GameFieldCells[(i - j), j].innerValue)
                        {
                            break;
                        }
                        if (j == GameFieldSize.x - 1)
                        {
                            return true;
                        }
                    }
                }
            }

        }
        //Check vertical lines
        for (int j = 0; j < GameFieldSize.y; j++)
        {
            if (GameFieldCells[0, j].innerValue != null)
            {
                for (int i = 0; i < GameFieldSize.x; i++)
                {
                    if (GameFieldCells[0, j].innerValue != GameFieldCells[i, j].innerValue)
                    {
                        break;
                    }
                    if (i == GameFieldSize.x - 1)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    /*
    private bool CheckValues(int firstIndex, int secondIndex)
    {
        string firstValue = buttons[firstIndex].GetComponentInChildren<Button>().GetComponentInChildren<Image>().gameObject.tag;
        string secondValue = buttons[secondIndex].GetComponentInChildren<Button>().GetComponentInChildren<Image>().gameObject.tag;

        if (firstValue == "" || secondValue == "")
            return false;
        if (firstValue == secondValue)
            return true;
        else
            return false;
    }*/

}
