using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BasicState
{
    public string stateName = "";
    public AssemblyLineManager assemblyLineManager;
    /// <summary>
    /// Indicates which body part should be going through the factory right now. 
    /// So always either Head, tails or body
    /// </summary>
    public string bodyPartStep = "";

    public BasicState (AssemblyLineManager assemblyLineManager)
    {
        this.assemblyLineManager = assemblyLineManager;
        stateName = nameof (BasicState);
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }


    protected void MoveCurrentBodyPart(Vector3 target, float time)
    {
        assemblyLineManager.currentBuildingBlock.transform.DOMove(target, time);

    }
}
