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
        Tween moveIntoMachine = MoveCurrentBodyPart(assemblyLineManager.inMachinePoint.position, 1f);
        moveIntoMachine.OnComplete(() => InMachineDelegate());
    }

    public override void Exit()
    {
        assemblyLineManager.CreateBodyPart();
        assemblyLineManager.ResetMachine();
        StopAllEffects();
        assemblyLineManager.mashineWorking.Stop();
        DeleteBodyPartBase();
    }

    private void InMachineDelegate()
    {
        Tween moveThroughMachine = MoveCurrentBodyPart(assemblyLineManager.inMachinePoint.position + Vector3.right, assemblyLineManager.machineProcessingTime);
        StartAllEffects();
        assemblyLineManager.mashineWorking.Play();
        moveThroughMachine.OnComplete(() => assemblyLineManager.GoToState("CleanUp"));
    }

    private void StartAllEffects()
    {
        foreach (ParticleSystem eff in assemblyLineManager.workingStateParticleEffect)
        {
            eff.Play();
        }
    }

    private void StopAllEffects()
    {
        foreach (ParticleSystem eff in assemblyLineManager.workingStateParticleEffect)
        {
            eff.Stop();
        }
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

        if(assemblyLineManager.currentBuildingBlock.TryGetComponent<Animator>(out Animator temp))
        {
            MonoBehaviour.Destroy(temp);
        }
    }
}

