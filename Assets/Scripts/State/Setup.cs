using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : BasicState
{
    public Setup(AssemblyLineManager assemblyLineManager) : base(assemblyLineManager)
    {
        stateName = nameof(Setup);
    }

}
