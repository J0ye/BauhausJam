using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicSwitch : MonoBehaviour
{
    protected static List<BasicSwitch> _switches = new List<BasicSwitch>();

    protected int clicktAmount = 1;
    public UnityEvent onClick = new UnityEvent();

    public string bodyPartName = "";
    public string allowedState = "";
    public bool singleOn = false;

    protected void Awake()
    {
        _switches.Add(this);
    }

    public void OnMouseDown()
    {
        onClick.Invoke();
        clicktAmount++;
    }

    public virtual void Reset()
    {
        clicktAmount = 1;
    }

    public static void ResetSwitches()
    {
        foreach(BasicSwitch basicSwitch in _switches)
        {
            try
            {
                basicSwitch.Reset(); 
            }catch(SystemException e)
            {
                Debug.LogWarning("Caught error on reseting switches: " + e);
            }
        }
    }
}
