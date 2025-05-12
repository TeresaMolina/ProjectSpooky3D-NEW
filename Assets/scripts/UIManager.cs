using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject generatorPromptText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowPrompt()
    {
        if (generatorPromptText != null)
            generatorPromptText.SetActive(true);
    }

    public void HidePrompt()
    {
        if (generatorPromptText != null)
            generatorPromptText.SetActive(false);
    }
}