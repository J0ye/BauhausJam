using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;


public class AssemblyLineManager : MonoBehaviour
{
    private float legAmount = 0;
    private Dictionary<string, SwitchData> bodyPartData = new Dictionary<string, SwitchData>();
    public GameObject bodyPartPrefab;
    public BasicBodyPart currentBuildingBlock { get; private set; }
    public List<string> bodyParts = new List<string>();

    public List<Transform> endPoints = new List<Transform>();
    public List<GameObject> existingBodys = new List<GameObject>();

    private BasicState currentState;
    public BasicState CurrentState { get { return currentState; } }


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

        foreach (string part in bodyParts)
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
            currentBuildingBlock.SwitchBodyPartAmount(entry.Key, entry.Value);
            print($"Switching: {entry.Key} to {entry.Value}");
        }
    }

    public void GoToState(string stateName)
    {
        if (currentState.stateName != stateName)
        {
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

    public void PrepNewBody()
    {
        currentBuildingBlock = Instantiate(bodyPartPrefab, bodyPartPrefab.transform.position, bodyPartPrefab.transform.rotation).GetComponent<BasicBodyPart>();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AssemblyLineManager))]
public class MyComponentEditor : Editor
{

    public override void OnInspectorGUI()
    {
        AssemblyLineManager myComponent = (AssemblyLineManager)target;

        // Draw a text field in the inspector
        if (myComponent.CurrentState != null)
        {
            EditorGUILayout.TextArea(myComponent.CurrentState.stateName);
        }
        else
        {
            EditorGUILayout.TextArea("State", "Enter play modus");
        }

        DrawDefaultInspector();
    }
}
#endif
