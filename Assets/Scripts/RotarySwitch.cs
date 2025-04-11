using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarySwitch : BasicSwitch
{
    // Start is called before the first frame update
    void Start()
    {
        onClick.AddListener(OnSwitch);
    }

    public void OnSwitch()
    {

    }
}
