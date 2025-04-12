using DG.Tweening;
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
        Tween moveIntoMachine = MoveCurrentBodyPart(assemblyLineManager.inMachinePoint.position, 2f);
        moveIntoMachine.OnComplete(() => InMachineDelegate());
    }

    public override void Exit()
    {
        assemblyLineManager.CreateBodyPart();
        assemblyLineManager.ResetMachine();
        assemblyLineManager.smoke.Stop();
        DeleteBodyPartBase();
    }

    private void InMachineDelegate()
    {
        Tween moveThroughMachine = MoveCurrentBodyPart(assemblyLineManager.inMachinePoint.position + Vector3.right, 5f);
        assemblyLineManager.smoke.Play();
        moveThroughMachine.OnComplete(() => assemblyLineManager.GoToState("CleanUp"));
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

