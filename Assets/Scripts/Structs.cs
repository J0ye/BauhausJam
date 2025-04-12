using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwitchData {

    public string bodyPart;
    public int buttonData;
    public string allowedState;
    public bool singleSelection;
    public BodyPartStep allowedBodyPartStep;

    public SwitchData()
    {
        bodyPart = string.Empty;
        buttonData = 0;
        allowedState = string.Empty;
        singleSelection = false;
        allowedBodyPartStep = BodyPartStep.Head;
    }
}
