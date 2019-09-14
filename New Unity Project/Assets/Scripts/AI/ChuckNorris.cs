using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckNorris : AbstractAI
{
    public override Cell CalculateMoveCell()
    {
        if (!IsThereAnyFreeCell())
        {
            GameManager.instance.Increment(0, 0, 1);
            throw new System.Exception("There is no free cell in game field");
        }
        //TODO Make very difficult AI
        throw new System.NotImplementedException();
    }
}
