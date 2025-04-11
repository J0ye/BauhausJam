using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType {Head, Body, Tail };
public class BasicBodyPart : MonoBehaviour
{
    public BodyType type;
    public Dictionary<string, GameObject> bodyParts = new Dictionary<string, GameObject>();

    public void SwitchBodyPart(string partName, bool newState)
    {
        if(bodyParts.ContainsKey(partName))
        {
            bodyParts[partName].SetActive(newState);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
