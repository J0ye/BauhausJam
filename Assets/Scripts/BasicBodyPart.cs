using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BodyType {Head, Body, Tail };
public class BasicBodyPart : MonoBehaviour
{
    public BodyType type;
    public Dictionary<string, GameObject> bodyParts = new Dictionary<string, GameObject>();

    public void SwitchBodyPart(string partName, bool newState)
    {
        if(bodyParts.ContainsKey(partName))
        {
            bodyParts[partName].SetActive(newState);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SwitchBodyPartAmount(string bodyPart, float value)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (KeyValuePair<string, GameObject> pair in bodyParts)
        {
            if (pair.Key.ToLower().Contains(bodyPart))
            {
                list.Add(pair.Value);
            }
        }

        list.Shuffle();

        value = Mathf.Clamp(value, 0, list.Count);

        for (int i = 0; i < value; i++)
        {
            list[i].SetActive(true);
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
            int k = Random.Range(0, n +1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
