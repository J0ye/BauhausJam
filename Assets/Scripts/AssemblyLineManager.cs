using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class AssemblyLineManager : MonoBehaviour
{
    private float legAmount = 0;
    private Dictionary<string, SwitchData> bodyPartData = new Dictionary<string, SwitchData>();
    public BasicBodyPart currentBuildingBlock;
    public List<string> bodyParts = new List<string>();

    

    private BasicState currentState;


    public static AssemblyLineManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = new AssemblyLineManager();
        }
        else
        {
            Destroy(this);
        }
        currentState = new Setup(this);
        currentState.Enter();

        foreach(string part in bodyParts)
        {
            bodyPartData.Add(part, new SwitchData());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBodyPartDate(SwitchData dataIn)
    {
        if (bodyPartData.ContainsKey(dataIn.bodyPart) && currentState.stateName == dataIn.allowedState)
        {
            bodyPartData[dataIn.bodyPart] = dataIn;
            print("data set");
        }
        Debug.Log("Recived:" + dataIn.bodyPart + dataIn.buttonData + dataIn.allowedState);
        Debug.Log(currentState.stateName);
        //Muss wieder RAUS
        //CreateBodyPart();
    }

    public void CreateBodyPart()
    {
        foreach (KeyValuePair<string, SwitchData> entry in bodyPartData)
        {
            currentBuildingBlock.SwitchBodyPartAmount(entry.Key,entry.Value);
            print($"Switching: {entry.Key} to {entry.Value}");
        }
    }

    public void GoTo(string stateName)
    {
        if (currentState.stateName != stateName) { 
            switch (stateName.ToLower())
            {
                case "setup":
                    ChangeState(new Setup(this));
                    break;

                case "working":
                    ChangeState(new Working(this));
                    break;

                case "cleanup":
                    ChangeState(new CleanUp(this));
                    break;
            }
        }
    }

    protected void ChangeState(BasicState bs)
    {
        currentState.Exit();
        currentState = bs;
        currentState.Enter();
    }
}
