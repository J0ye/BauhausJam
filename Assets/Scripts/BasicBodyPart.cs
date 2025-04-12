using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BasicBodyPart : MonoBehaviour
{
    public List<GameObject> bodyParts = new List<GameObject>();
    public Dictionary<string, GameObject> bodyPartsAndNames = new Dictionary<string, GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gameObject in bodyParts)
        {
            bodyPartsAndNames[gameObject.name] = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchBodyPart(string partName, bool newState)
    {
        if (bodyPartsAndNames.ContainsKey(partName))
        {
            print($"Switching: {partName}");
            bodyPartsAndNames[partName].SetActive(newState);
        }
    }
    public void SwitchBodyPartAmount(string bodyPart, SwitchData value)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (KeyValuePair<string, GameObject> pair in bodyPartsAndNames)
        {
            print("Checking " + bodyPart.ToLower() + " with " + pair.Key.ToLower());
            if (pair.Key.ToLower().Contains(bodyPart.ToLower()))
            {
                list.Add(pair.Value);
            }
        }


        if (!value.singleSelection)
        {
            list.Shuffle();
        }
        foreach (GameObject gameObject in list)
        {
            gameObject.SetActive(false);
        }

        value.buttonData = Mathf.Clamp(value.buttonData, 0, list.Count);

        if (value.buttonData != 0)
        {
            if (value.singleSelection)
            {
                list[value.buttonData - 1].SetActive(true);
            }
            else
            {
                for (int i = 0; i < value.buttonData; i++)
                {
                    list[i].SetActive(true);
                }
            }
        }


    }


}
public static class ListExtensions
{

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
