using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : BasicState
{
    public Setup(AssemblyLineManager assemblyLineManager) : base(assemblyLineManager)
    {
        stateName = nameof(Setup);
    }

    public override void Enter()
    {
        assemblyLineManager.PrepNewBody();
        MoveCurrentBodyPart(assemblyLineManager.bodyPartPrefab.transform.position, 1f);
    }
}
