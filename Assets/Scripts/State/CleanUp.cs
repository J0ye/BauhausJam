using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Net;

public class CleanUp : BasicState
{
    
    public CleanUp(AssemblyLineManager assemblyLineManager) : base(assemblyLineManager)
    {
        stateName = nameof(CleanUp);
    }

    public override void Enter()
    {
        Tween moveToEndOfBelt = MoveCurrentBodyPart(assemblyLineManager.endPoint.position, 2f);
        assemblyLineManager.existingBodys.Add(assemblyLineManager.currentBuildingBlock.gameObject);
        moveToEndOfBelt.OnComplete(() => assemblyLineManager.GoToState("Setup"));
    }

    public override void Exit()
    {
        MoveCurrentBodyPart(assemblyLineManager.endPoint.position + Vector3.right * 5, 1f);
    }
}
