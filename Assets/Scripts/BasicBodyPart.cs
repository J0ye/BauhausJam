using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BasicBodyPart : MonoBehaviour
{

    public List<GameObject> bodyParts = new List<GameObject>();
    public Dictionary<string, GameObject> bodyPartsAndNames = new Dictionary<string, GameObject>();
    public List<SwitchData> switchData {  get; private set; }

    private bool isVeryExtra = false;


    public void SwitchBodyPart(string partName, bool newState)
    {
        foreach (GameObject gameObject in bodyParts)
        {
            if (gameObject.name == partName)
            {
                gameObject.SetActive(newState);
            }
        }
    }

    public void SwitchBodyPartAmount(SwitchData value)
    {
        print($"Switch request for data (bp:{value.bodyPart}, data:{value.buttonData}, " +
            $"target state:{value.allowedState}, single {value.singleSelection.ToString()} )");

        List<GameObject> list = GetListOfParts(value.bodyPart);

        if (!value.singleSelection)
        {
            list.Shuffle();
        }
        //DeactivateAll();
        value.buttonData = Mathf.Clamp(value.buttonData, 0, list.Count - 1);

        if (!isVeryExtra)
        {
            // Only manage bodyparts if the egg isnt with extra parts
            if (value.singleSelection)
            {
                list[value.buttonData].SetActive(true);
            }
            else
            {
                for (int i = 0; i <= value.buttonData; i++)
                {
                    list[i].SetActive(true);
                }
            }
        }

        if(switchData == null)
        {
            switchData = new List<SwitchData>();
        }

        if(value.bodyPart.ToLower() == "extra")
        {
            isVeryExtra = true;
            foreach(SwitchData d in switchData)
            {
                if(!d.IsMainPart())
                {
                    // deactivate everything but the main parts and extra crazy limbs
                    List<GameObject> temp = GetListOfParts(d.bodyPart); 
                    temp[d.buttonData].SetActive(false);
                }
            }
        }
        switchData.Add(value);
    }

    public List<GameObject> GetListOfParts(string bodyPartName)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (GameObject gbj in bodyParts)
        {
            if (gbj.name.ToLower().Contains(bodyPartName.ToLower()))
            {
                list.Add(gbj);
            }
        }
        return list;
    }

    public void DeactivateAll()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
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
