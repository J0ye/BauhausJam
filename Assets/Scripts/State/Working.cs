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
        assemblyLineManager.CreateBodyPart();
    }
}
