using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // notwendig f�r Button

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
