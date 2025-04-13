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
        
        assemblyLineManager.wormSound.clip = assemblyLineManager.wormAudio[GetWeightedRandomInt(assemblyLineManager.gewichteteWarscheinlichkeitAudio)];
        assemblyLineManager.wormSound.Play();
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
            MoveCurrentBodyPart(assemblyLineManager.endPoint.position + Vector3.right * 5, 5f);
        }

    }

    public int GetWeightedRandomInt(float weightForHighNumbers)
    {

        // Interner Bias-Wert: >1 = 4-6 häufiger, <1 = 4-6 seltener
        float biasForHighNumbers = 2.0f;

        // Gewichte für 0 bis 6
        float[] weights = new float[]
        {
        1f, // 0
        1f, // 1
        1f, // 2
        1f, // 3
        1f * biasForHighNumbers, // 4
        1f * biasForHighNumbers, // 5
        1f * biasForHighNumbers  // 6
        };

        // Gesamtgewicht berechnen
        float totalWeight = 0f;
        foreach (float w in weights)
            totalWeight += w;

        // Zufallswert im Bereich des Gesamtgewichts
        float randomValue = UnityEngine.Random.Range(0f, totalWeight);
        float cumulative = 0f;

        // Wähle Zahl basierend auf Gewicht
        for (int i = 0; i < weights.Length; i++)
        {
            cumulative += weights[i];
            if (randomValue < cumulative)
                return i;
        }

        return 0; // Fallback (eigentlich nie erreicht)
    }

}
