using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAI : MonoBehaviour
{
    public abstract Cell CalculateMoveCell();

    public bool IsThereAnyFreeCell()
    {
        bool retValue = false;
        MovingLogic.instance.grid.buttons.ForEach(x => 
        {
            if(x.innerValue == null)
            {
                retValue = true;
                return;
            }
        });
        return retValue;
    }
}
