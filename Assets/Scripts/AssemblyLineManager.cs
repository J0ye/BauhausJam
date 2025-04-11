using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssemblyLineManager : MonoBehaviour
{
    private float legAmount = 0;
    private Dictionary<string, float> bodyPartData = new Dictionary<string, float>();
    public BasicBodyPart currentBuildingBlock;


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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBodyPartDate(string bodyPart, float amount)
    {
        if (bodyPartData.ContainsKey(bodyPart))
        {
            bodyPartData[bodyPart] = amount;
        }
    }

    public void CreateBodyPart()
    {
        foreach (KeyValuePair<string, float> entry in bodyPartData)
        {
            currentBuildingBlock.SwitchBodyPartAmount(entry.Key,entry.Value);
        }
    }
}
