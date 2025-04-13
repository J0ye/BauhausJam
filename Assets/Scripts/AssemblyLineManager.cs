using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using UnityEditor;
using UnityEngine;

public enum BodyPartStep { Head, Tail, Body }

public class AssemblyLineManager : MonoBehaviour
{
    public GameObject bodyPartPrefab;
    public BasicBodyPart currentBuildingBlock { get; private set; }
    public List<GameObject> existingBodys = new List<GameObject>();
    public List<string> bodyParts = new List<string>();
    public Transform inMachinePoint;
    public Transform endPoint;
    [Tooltip("Used for the duration of the animation throuhg the machine")]
    public float machineProcessingTime = 3f;

    [Header("Machine effects")]
    public List<ParticleSystem> workingStateParticleEffect;
    public AudioSource mashineWorking;

    private List<SwitchData> bodyPartData = new List<SwitchData>();

    public BasicState CurrentState { get { return currentState; } }
    private BasicState currentState;


    public static AssemblyLineManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        currentState = new Setup(this);
        currentState.Enter();
        print("in manager " + bodyParts.Count);
        FillBodyList();
    }

    public void SetBodyPartDate(SwitchData dataIn)
    {
        for (int i = 0; i < bodyPartData.Count; i++)
        {
            if (string.Equals(bodyPartData[i].bodyPart, dataIn.bodyPart, StringComparison.OrdinalIgnoreCase))
            {
                bodyPartData[i] = dataIn;
                break; // Exit early. Only one match expected
            }
        }
    }

    public void CreateBodyPart()
    {
        foreach (SwitchData entry in bodyPartData)
        {
            bool isMainBodyPart = entry.IsMainPart();


            if (!isMainBodyPart && entry.buttonData > 0)
            {
                entry.buttonData--;
                currentBuildingBlock.SwitchBodyPartAmount(entry);
            }
            else if(isMainBodyPart)
            {
                currentBuildingBlock.SwitchBodyPartAmount(entry);
            }
        }
    }

    public void StartMachine()
    {
        if(currentState.stateName.ToLower() == "setup")
        {
            GoToState("Working");
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
    public void PrepNewBody()
    {
        Vector3 offsetToOffscreen = Vector3.right * -2;
        currentBuildingBlock = Instantiate(bodyPartPrefab, bodyPartPrefab.transform.position + offsetToOffscreen, bodyPartPrefab.transform.rotation).GetComponent<BasicBodyPart>();
    }

    public void ResetMachine()
    {
        FillBodyList();
        BasicSwitch.ResetSwitches();
    }

    public void ClearSpawnedBodies()
    {
        foreach (GameObject obj in existingBodys)
        {
            Destroy(obj);
        }
        existingBodys.Clear();
    }

    protected void ChangeState(BasicState bs)
    {
        currentState.Exit();
        currentState = bs;
        currentState.Enter();
    }

    protected void FillBodyList()
    {
        bodyPartData.Clear();
        foreach (string part in bodyParts)
        {
            SwitchData temp = new SwitchData();
            temp.singleSelection = true;
            temp.bodyPart = part;
            temp.allowedState = "setup";
            bodyPartData.Add(temp);
        }
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
            EditorGUILayout.TextArea("Enter play modus");
        }

        DrawDefaultInspector();
    }
}
#endif
