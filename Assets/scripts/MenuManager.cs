using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // This string must exactly match your scene name:
    [SerializeField] private string levelSceneName = "inside corridor";

    /// <summary>
    /// Hook this up on your Play button’s OnClick.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(levelSceneName);
    }

    /// <summary>
    /// Hook this up on your Exit button’s OnClick if you want it to quit.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
