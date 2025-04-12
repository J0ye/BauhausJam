using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderSwitch : BasicSwitch
{
    public float sliderSize = 1f;

    private Vector3 startPos;


    private void Start()
    {
        startPos = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
    }

    private void LateUpdate()
    {
        
    }
}
