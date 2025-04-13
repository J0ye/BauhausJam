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

    // Start is called before the first frame update
    protected void Start()
    {
        onClick.AddListener(OnSwitch);
    }

    public virtual void OnSwitch()
    {
        int degrees = 360 / turnAmount;
        int i = clicktAmount % turnAmount;

        transform.DORotate(new Vector3(0, 0, degrees * -i), 0.1f);

        SwitchData data = new SwitchData();
        data.bodyPart = bodyPartName;
        data.buttonData = i; // 0 if button is back to start
        data.allowedState = allowedState;
        data.singleSelection = singleOn;
        data.allowedBodyPartStep = targetStep;

        onRotate.Invoke(data);
    }

    public override void Reset()
    {
        base.Reset();
        transform.DORotate(new Vector3(0, 0, 0), 0.1f); // Turn to zero
    }
}
