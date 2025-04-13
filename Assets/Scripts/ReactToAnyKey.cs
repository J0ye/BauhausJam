using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class ReactToAnyKey : MonoBehaviour
{

    public UnityEvent onAnyButton = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            onAnyButton.Invoke();
        }
    }
}
