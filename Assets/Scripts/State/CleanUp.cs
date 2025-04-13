using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Net;
using static UnityEngine.RuleTile.TilingRuleOutput;
using TMPro;

public class CleanUp : BasicState
{
    private bool questResult = false;
    public CleanUp(AssemblyLineManager assemblyLineManager) : base(assemblyLineManager)
    {
        stateName = nameof(CleanUp);
    }

    public override void Enter()
    {
        Tween moveToEndOfBelt = MoveCurrentBodyPart(assemblyLineManager.endPoint.position, 3f);
        assemblyLineManager.existingBodys.Add(assemblyLineManager.currentBuildingBlock.gameObject);
        questResult = QuestBoardController.Instance.CompareWithQuest(assemblyLineManager.currentBuildingBlock);
        Debug.Log("And the result is: " + questResult);

        moveToEndOfBelt.OnComplete(() => SittingStill());
    }

    public void SittingStill()
    {

        assemblyLineManager.GoToState("Setup");
    }

    public override void Exit()
    {
        if (questResult)
        {
            int randX = Random.Range(-4, 2);
            Vector3 targetPos = assemblyLineManager.endPoint.position + new Vector3(randX, 3, 0) * 5;
            Vector3 direction = targetPos - assemblyLineManager.currentBuildingBlock.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            MoveCurrentBodyPart(targetPos, 4f);
            //assemblyLineManager.currentBuildingBlock.transform.DOLookAt(new Vector3(0, 0, angle), 0.1f);
            assemblyLineManager.currentBuildingBlock.transform.DOShakeScale(4f);
            QuestBoardController.Instance.CallNewQuest();
        }
        else
        {
            // Move to the right of the screen
            MoveCurrentBodyPart(assemblyLineManager.endPoint.position + Vector3.right * 5, 1f);
        }

    }
}
