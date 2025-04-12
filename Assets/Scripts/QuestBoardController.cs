using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.EventSystems.EventTrigger;

public class QuestBoardController : MonoBehaviour
{
    public BasicBodyPart questDisplay;
    public List<SwitchData> questData = new List<SwitchData>();
    public Vector3 targetMove = Vector3.up;  

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        SetNewQuest();
        SetQuestDisplay();
    }

    private void OnMouseEnter()
    {
        if (transform.position == startPos)
        {
            OpenScreen();
        }
    }

    private void OnMouseExit()
    {
        if (transform.position != startPos)
        {
            CloseScreen();
        }
    }
    
    protected void SetNewQuest()
    {
        questData.Clear();

        foreach (string bodyPartName in AssemblyLineManager.instance.bodyParts)
        {
            int maxCount = questDisplay.GetListOfParts(bodyPartName).Count;
            SwitchData randomData = new SwitchData();
            randomData.bodyPart = bodyPartName;
            randomData.buttonData = Random.Range(0, maxCount);
            randomData.singleSelection = true;
            questData.Add(randomData);
        }
    }

    protected void SetQuestDisplay()
    {
        questDisplay.DeactivateAll();
        foreach(SwitchData sd in questData)
        {
            questDisplay.SwitchBodyPartAmount(sd);
        }
    }

    protected void CloseScreen()
    {
        transform.DOMove(startPos, 0.2f);
    }

    protected void OpenScreen()
    {
        transform.DOMove(startPos + targetMove, 0.5f);
    }
}
