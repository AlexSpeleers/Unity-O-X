using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ez : AbstractAI
{
    public override Cell CalculateMoveCell()
    {
        if (!IsThereAnyFreeCell())
        {
            GameManager.instance.Increment(0, 0, 1);
            MovingLogic.instance.EndGameBoard.GetComponentInChildren<TMP_Text>().text = "DRAW";
            MovingLogic.instance.EndGameBoard.transform.localScale = Vector2.one;
        }

        int randomCellNum = -1;
        do
        {
            int tempNum = Random.Range(0, MovingLogic.instance.grid.buttons.Count);
            if (MovingLogic.instance.grid.buttons[tempNum].innerValue == null)
            {
                randomCellNum = tempNum;
            }
        }
        while (randomCellNum <= 0);

        return MovingLogic.instance.grid.buttons[randomCellNum];
    }
}
