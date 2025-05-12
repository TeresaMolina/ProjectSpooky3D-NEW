using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorTrigger : MonoBehaviour
{
    public string sceneToLoad = "main"; // or your actual menu scene
    public float interactRange = 2f;
    public KeyCode interactKey = KeyCode.E;
    public Transform player;

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
