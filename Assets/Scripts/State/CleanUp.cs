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
        int i = assemblyLineManager.existingBodys.Count;
        assemblyLineManager.currentBuildingBlock.transform.DOMove(assemblyLineManager.endPoints[i].position, 2f);
        assemblyLineManager.existingBodys.Add(assemblyLineManager.currentBuildingBlock.gameObject);
        assemblyLineManager.GoToState("Setup");
    }
}
