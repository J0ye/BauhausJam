using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SliderSwitch : BasicSwitch
{
    public float sliderSize = 1f;
    public int sliderAmount = 3;

    public SwitchEvent OnSlide = new SwitchEvent();
    

    private Vector3 startPos;
    private int buttonState = 0;

    private void Start()
    {
        startPos = transform.position;
        clickIntensity = new Vector3(0.8f, 0.8f, 0.8f);
    }

    private void OnMouseDrag()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector2 clamped = ClampToLine(startPos, startPos + transform.right * sliderSize, mouseWorldPos);
        Vector2 snapped = GetClosesedPosition(GetPossibleSliderPos(), clamped);
        transform.position = new Vector3(snapped.x, snapped.y, transform.position.z);
        CallEvent(buttonState);
    }

    Vector2 ClampToLine(Vector2 pointA, Vector2 pointB, Vector2 targetPosition)
    {
        Vector2 AB = pointB - pointA;
        Vector2 AP = targetPosition - pointA;

        float t = Vector2.Dot(AP, AB.normalized) / AB.magnitude; // or: float t = Vector2.Dot(AP, AB) / AB.sqrMagnitude;

        // Clamp 't' between 0 and 1 to stay on the segment
        t = Mathf.Clamp01(t);

        return pointA + AB * t;
    }

    private List<Vector3> GetPossibleSliderPos() {
        List<Vector3> pos = new List<Vector3>();

        pos.Add(startPos);
        float steps = sliderSize / sliderAmount;

        for (int i = 1;i < sliderAmount; i++)
        {
            pos.Add(startPos + transform.right * (steps* i));
        }
        return pos;
    }

    public Vector3 GetClosesedPosition(List<Vector3> posList, Vector3 value)
    {
        Vector3 ret = posList[0];
        int counter = 0;
        buttonState = counter;
        foreach (Vector3 p in posList)
        {
            float dis=Vector3.Distance(p, value);
            float dis2 = Vector3.Distance(ret, value);

            if (dis2 > dis) { 
            
                ret = p;
                buttonState = counter;
            }
            counter++;
        }

        return ret;
    }

    protected void CallEvent(int i)
    {
        SwitchData data = new SwitchData();
        data.bodyPart = bodyPartName;
        data.buttonData = i;
        data.allowedState = allowedState;
        data.singleSelection = singleOn;
        data.allowedBodyPartStep = targetStep;

        OnSlide.Invoke(data);
    }

    public override void Reset()
    {
        base.Reset();
        transform.position = startPos; // Move back to where switch started
        CallEvent(0);
    }
}
