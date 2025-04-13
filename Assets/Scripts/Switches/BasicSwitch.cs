using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicSwitch : MonoBehaviour
{
    protected static List<BasicSwitch> _switches = new List<BasicSwitch>();

    public UnityEvent onClick = new UnityEvent();
    public BodyPartStep targetStep = BodyPartStep.Head;
    public string bodyPartName = "";
    public string allowedState = "";
    public bool singleOn = false;

    protected AudioSource audioSourceOnClick;
    protected Tween clickAnimation;
    protected Vector3 clickIntensity = new Vector3(0.2f, 0.2f, 0.2f);
    protected int clicktAmount = 1;

    protected void Awake()
    {
        _switches.Add(this);
        audioSourceOnClick = GetComponent<AudioSource>();
    }

    public void OnMouseDown()
    {
        onClick.Invoke();
        OnClick();
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

    public static void ResetSwitches(bool resetAllSwitches)
    {
        if (resetAllSwitches)
        {
            ResetSwitches();
        }
        else
        {
            print("i");
            // reset only non main part switches
            foreach (BasicSwitch basicSwitch in _switches)
            {
                SwitchData data = new SwitchData();
                data.bodyPart = basicSwitch.bodyPartName;
                if(!data.IsMainPart() && data.bodyPart.ToLower() != "extra")
                {
                    try
                    {
                        basicSwitch.Reset();
                    }
                    catch (SystemException e)
                    {
                        Debug.LogWarning("Caught error on reseting switches: " + e);
                    }
                }                
            }
        }

    }

    protected virtual void OnClick()
    {
        if (audioSourceOnClick != null)
        {
            float duration = audioSourceOnClick.clip.length;
            StartAudio();
            Invoke(nameof(StopAudio), duration);
        }
        if(clickAnimation != null)
            clickAnimation.Complete();
        clickAnimation = transform.DOPunchScale(transform.localScale - clickIntensity, 0.2f);
    }

    protected void StartAudio()
    {
        if(audioSourceOnClick != null)
            audioSourceOnClick.Play();
    }

    protected void StopAudio()
    {
        if(audioSourceOnClick != null)
            audioSourceOnClick.Stop();
    }
}
