using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAge : AbstractAI
{
    public override Cell CalculateMoveCell()
    {
        if (!IsThereAnyFreeCell())
        { throw new System.Exception("There is no free cell in game field"); }
        //Make not so siple AI as 'Ez'
        throw new System.NotImplementedException();
    }
}
