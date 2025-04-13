using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // notwendig für Button

public class SceneChanger : MonoBehaviour
{
    [Header("Name der Zielszene (wie in Build Settings)")]
    public string sceneName;

    private void Start()
    {
        // Holt die Button-Komponente und fügt einen Listener hinzu
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ChangeScene);
        }
        else
        {
            Debug.LogWarning("Kein Button-Komponente gefunden auf diesem GameObject.");
        }
    }

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name ist leer! Bitte im Inspector angeben.");
        }
    }
}
