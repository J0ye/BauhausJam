using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : BasicState
{
    public CleanUp(AssemblyLineManager assemblyLineManager) : base(assemblyLineManager)
    {
        stateName = nameof(CleanUp);
    }
}
