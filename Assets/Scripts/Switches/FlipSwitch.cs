using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipSwitch : BasicSwitch
{
    public bool flipDownOnStart = false;
    public int forcedSwitchValue = 1;
    public SwitchEvent onFlip = new SwitchEvent();

    protected Vector3 startScale = Vector3.one;
    protected bool flipState = false;
    // Start is called before the first frame update
    void Start()
    {
        onClick.AddListener(OnFlip);
        if (flipDownOnStart)
        {
            FlipAnimation(); 
            Vector3 s = transform.localScale;
            Vector3 scale = new Vector3(s.x, s.y * -1, s.z);    
            startScale = scale;
        }
        else
        {
            startScale = transform.localScale;
        }
    }

    public virtual void OnFlip()
    {
        flipState = !flipState;
        int i = 0;
        if (flipState)
        {
            i = forcedSwitchValue;
        }
        FlipAnimation();

        CallEvent(i);
        if (isExtra)
        {
            ResetSwitches(false); // reset every switch but the main switches
        }
    }

    public override void Reset()
    {
        base.Reset();
        if (flipState)
        {
            // Only if the lever is down
            transform.DOScale(startScale, 0.1f);
            SwitchData data = new SwitchData();
            data.bodyPart = bodyPartName;
            data.buttonData = 0;
            data.allowedState = allowedState;
            data.singleSelection = singleOn;
            data.allowedBodyPartStep = targetStep;

            AssemblyLineManager.instance.SetBodyPartDate(data);
            flipState = !flipState;
        }
    }

    protected void FlipAnimation()
    {
        Vector3 s = transform.localScale;
        transform.DOScale(new Vector3(s.x, s.y * -1, s.z), 0.1f);
    }

    protected override void OnClick()
    {
        if (audioSourceOnClick != null)
        {
            float duration = audioSourceOnClick.clip.length;
            StartAudio();
            Invoke(nameof(StopAudio), duration);
        }
    }

    protected void CallEvent(int i)
    {
        SwitchData data = new SwitchData();
        data.bodyPart = bodyPartName;
        data.buttonData = i;
        data.allowedState = allowedState;
        data.singleSelection = singleOn;
        data.allowedBodyPartStep = targetStep;
        print("Flip event called with " + i);
        onFlip.Invoke(data);
    }
}
