using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicState
{
    public BasicState (AssemblyLineManager assemblyLineManager)
    {
        this.assemblyLineManager = assemblyLineManager;
        stateName = nameof (BasicState);
    }
    public AssemblyLineManager assemblyLineManager;

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }

    public string stateName = "";


}
