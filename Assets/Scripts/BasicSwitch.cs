using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicSwitch : MonoBehaviour
{
    protected int clicktAmount = 1;
    public UnityEvent onClick = new UnityEvent();

    public string bodyPartName = "";
    public string allowedState = "";
    public bool singleOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        onClick.Invoke();
        clicktAmount++;
    }
}
