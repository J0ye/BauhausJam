using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SwitchEvent : UnityEvent<SwitchData> { }



public class RotarySwitch : BasicSwitch
{
    
    public SwitchEvent onRotate = new SwitchEvent();

    public int turnAmount = 3;

    public string bodyPartName = "";
    public string allowedState = "";
    public bool singleOn = false;

    // Start is called before the first frame update
    void Start()
    {
        onClick.AddListener(OnSwitch);
    }

    public void OnSwitch()
    {
        int degrees = 360 / turnAmount;
        int i = clicktAmount % turnAmount;

        transform.DORotate(new Vector3(0, 0, degrees * -i), 0.1f);

        SwitchData data = new SwitchData();

        data.bodyPart = bodyPartName;
        data.buttonData = i;
        data.allowedState = allowedState;
        data.singleSelection = singleOn;

        onRotate.Invoke(data);
    }


}
