using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   // or TMPro

public class GameManager : MonoBehaviour {
  // ← add this line:
  public static GameManager Instance { get; private set; }

  [Header("Game Over UI")]
  public GameObject gameOverCanvas;
  public Image    gameOverImage;
  public Text     gameOverText;
  public AudioClip screamClip;

  AudioSource audioSource;

  void Awake() {
    // singleton setup:
    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);

    audioSource = gameObject.AddComponent<AudioSource>();
    audioSource.playOnAwake = false;
    gameOverCanvas.SetActive(false);
  }

  public void EndGame() {
    gameOverCanvas.SetActive(true);
    StartCoroutine(FadeInRoutine());
    audioSource.PlayOneShot(screamClip);
  }

  System.Collections.IEnumerator FadeInRoutine() {
    float t = 0f, duration = 1f;
    Color imgCol = gameOverImage.color, txtCol = gameOverText.color;
    while (t < duration) {
      t += Time.deltaTime;
      float a = Mathf.Clamp01(t/duration);
      gameOverImage.color = new Color(imgCol.r, imgCol.g, imgCol.b, a);
      gameOverText.color  = new Color(txtCol.r, txtCol.g, txtCol.b, a);
      yield return null;
    }
    yield return new WaitForSeconds(1f);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
