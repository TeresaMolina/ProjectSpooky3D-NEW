using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Control")]
    public GameObject player; // drag the Player prefab or scene object here

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip screamClip;

    [Header("Game Over UI")]
    [Tooltip("Drag your GameOverCanvas here (should contain a full-screen Image).")]
    public GameObject gameOverCanvas;

    [Tooltip("Drag the Image component inside that canvas here.")]
    public Image gameOverImage;

    [Header("Timing (seconds)")]
    [Tooltip("Seconds to fade in the image")]
    public float fadeDuration = 1f;

    [Header("Jumpscare Video")]
    public RawImage jumpscareImage;
    public VideoPlayer jumpscareVideo;


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
        //hide jumpscare image
        if(jumpscareVideo != null)
        {
            var color = jumpscareImage.color;
            color.a = 0f;
            jumpscareImage.color = color;
        }
    }

//add a guard to ensure only one mponster ends the game
    private bool isGameOver = false;

    public void EndGame()
    {

        if (player != null)
        {
            var move = player.GetComponent<PlayerMovement>();
            if (move != null) move.enabled = false;

            var look = player.GetComponentInChildren<MouseLook>();
            if (look != null) look.enabled = false;

            var flashlight = player.GetComponentInChildren<FlashlightController>();
            if (flashlight != null)
            {
                // force flicker and turn off
                Light lightComp = flashlight.flashlight;
                if (lightComp != null)
                {
                    lightComp.enabled = false;
                }
                flashlight.enabled = false;
            }

        }


        if (isGameOver) return; // prevent double-trigger
        isGameOver = true;

        if (gameOverCanvas == null || gameOverImage == null)
        {
            Debug.LogError("GameManager: Missing GameOverCanvas or GameOverImage!");
            return;
        }

        if (audioSource != null && screamClip != null)
        {
            audioSource.PlayOneShot(screamClip);
        }


        StartCoroutine(GameOverSequence());
    }



    /// <summary>
    /// Call this when the monster touches the player.
    /// </summary>
    //public void EndGame()
    //{
    //    if (gameOverCanvas == null || gameOverImage == null)
    //    {
    //        Debug.LogError("GameManager: Missing GameOverCanvas or GameOverImage!");
    //        return;
    //    }
    //    StartCoroutine(GameOverSequence());
    //}

    private IEnumerator GameOverSequence()
    {
        gameOverCanvas.SetActive(true);

        // 0) show jumpscare video (if assigned)
        if (jumpscareImage != null && jumpscareVideo != null)
        {
            // make video visible
            jumpscareImage.color = Color.white;

            // play video (with sound using Direct mode)
            jumpscareVideo.Play();

            // wait for 5 seconds (your video's length)
            //yield return new WaitForSeconds(5f);
            yield return new WaitForSeconds((float)jumpscareVideo.length);


            // optional: hide video after it finishes
            jumpscareImage.color = new Color(1, 1, 1, 0);
        }

        // 1) fade in game over image
        float t = 0f;
        Color col = gameOverImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            col.a = Mathf.Clamp01(t / fadeDuration);
            gameOverImage.color = col;
            yield return null;
        }

        col.a = 1f;
        gameOverImage.color = col;

        // 2) wait
        yield return new WaitForSeconds(displayDuration);

        // 3) return to main menu
        SceneManager.LoadScene("main");
    }


    //private IEnumerator GameOverSequence()
    //{
    //    // 1) activate the canvas
    //    gameOverCanvas.SetActive(true);

    //    // 2) fade the image in
    //    float t = 0f;
    //    Color col = gameOverImage.color;
    //    while (t < fadeDuration)
    //    {
    //        t += Time.deltaTime;
    //        col.a = Mathf.Clamp01(t / fadeDuration);
    //        gameOverImage.color = col;
    //        yield return null;
    //    }
    //    // ensure fully opaque
    //    col.a = 1f;
    //    gameOverImage.color = col;

    //    // 3) wait
    //    yield return new WaitForSeconds(displayDuration);

    //    // 4) go back to main menu
    //    SceneManager.LoadScene("main");
    //}
}
