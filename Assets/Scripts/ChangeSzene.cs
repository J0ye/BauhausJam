using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // notwendig für Button

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
