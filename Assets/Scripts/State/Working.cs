using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Working : BasicState
{
    public Working(AssemblyLineManager assemblyLineManager) : base(assemblyLineManager)
    {
        stateName = nameof(Working);
    }

    public override void Enter()
    {
        MoveCurrentBodyPart(assemblyLineManager.inMachinePoint.position, 2f);
    }

    public override void Exit()
    {
        assemblyLineManager.CreateBodyPart();
        assemblyLineManager.ResetMachine();
        DeleteBodyPartBase();
    }

    private void DeleteBodyPartBase()
    {
        foreach (Transform c in assemblyLineManager.currentBuildingBlock.transform)
        {
            if(c.name == "Base")
            {
                MonoBehaviour.Destroy(c.gameObject);
            }
        }
    }
}

