using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.EventSystems.EventTrigger;
using System.Linq;
using TMPro;

public class QuestBoardController : MonoBehaviour
{
    public static QuestBoardController Instance { get; private set; }

    public BasicBodyPart questDisplay;
    public List<SwitchData> questData = new List<SwitchData>();
    public TMP_Text resultText;
    public Vector3 targetMove = Vector3.up;

    private int filledQuestCounter = 0;

    private Vector3 startPos;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(this); 
        }
    }

    private void Start()
    {
        startPos = transform.position;
        SetNewQuest();
        SetQuestDisplay();

        if (resultText != null)
        {
            resultText.text = "Filled Quests " + filledQuestCounter;
        }
    }

    public bool CompareWithQuest(BasicBodyPart buildBody)
    {
        int count = 0;
        foreach (SwitchData bodyDataUnit in buildBody.switchData)
        {
            // get every entry of the quest, where name of bodypart = this iterator
            var matches = questData.Where(quest => quest.bodyPart == bodyDataUnit.bodyPart);
            foreach (var match in matches)
            {
                // matching body part
                if (match.buttonData == bodyDataUnit.buttonData)
                {
                    // same body part and same data indicats matching body part
                    count++;
                }
            }
        }

        print("Found: " +  count + " right body parts");
        if (count == buildBody.switchData.Count)
        {
            // There are as many right entries in the quest as there a entries in the build body
            filledQuestCounter++;
            if(resultText != null)
            {
                resultText.text = "Filled Quests " + filledQuestCounter;
            }
            return true;
        }
        else
        {
            return false;
        }
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
        foreach (SwitchData sd in questData)
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
