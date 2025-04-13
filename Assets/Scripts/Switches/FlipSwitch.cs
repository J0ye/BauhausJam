using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipSwitch : BasicSwitch
{
    public bool flipDownOnStart = false;
    public SwitchEvent onFlip = new SwitchEvent();

    protected Vector3 startScale = Vector3.one;
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
        int i = clicktAmount % 2;
        FlipAnimation();

        SwitchData data = new SwitchData();
        data.bodyPart = bodyPartName;
        data.buttonData = i;
        data.allowedState = allowedState;
        data.singleSelection = singleOn;
        data.allowedBodyPartStep = targetStep;

        print("Flip i is " + i);
        onFlip.Invoke(data);
    }

    public override void Reset()
    {
        base.Reset();
        transform.DOScale(startScale, 0.1f); // Turn to zero
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
}
