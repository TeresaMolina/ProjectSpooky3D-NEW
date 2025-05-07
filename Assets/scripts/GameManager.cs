using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Over UI")]
    [Tooltip("Drag your GameOverCanvas here (should contain a full-screen Image).")]
    public GameObject gameOverCanvas;

    [Tooltip("Drag the Image component inside that canvas here.")]
    public Image gameOverImage;

    [Header("Timing (seconds)")]
    [Tooltip("Seconds to fade in the image")]
    public float fadeDuration = 1f;
    [Tooltip("Seconds to hold full opacity before returning to main menu")]
    public float displayDuration = 5f;

    private void Awake()
    {
        // singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // hide at start
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);

        if (gameOverImage != null)
        {
            var c = gameOverImage.color;
            c.a = 0f;
            gameOverImage.color = c;
        }
    }

    /// <summary>
    /// Call this when the monster touches the player.
    /// </summary>
    public void EndGame()
    {
        if (gameOverCanvas == null || gameOverImage == null)
        {
            Debug.LogError("GameManager: Missing GameOverCanvas or GameOverImage!");
            return;
        }
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        // 1) activate the canvas
        gameOverCanvas.SetActive(true);

        // 2) fade the image in
        float t = 0f;
        Color col = gameOverImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            col.a = Mathf.Clamp01(t / fadeDuration);
            gameOverImage.color = col;
            yield return null;
        }
        // ensure fully opaque
        col.a = 1f;
        gameOverImage.color = col;

        // 3) wait
        yield return new WaitForSeconds(displayDuration);

        // 4) go back to main menu
        SceneManager.LoadScene("main");
    }
}
