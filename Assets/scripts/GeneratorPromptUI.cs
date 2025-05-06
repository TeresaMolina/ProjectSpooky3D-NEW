using UnityEngine;
using TMPro;

public class GeneratorPromptUI : MonoBehaviour
{
    public TextMeshProUGUI promptText;

    public void ShowPrompt()
    {
        promptText.gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }
}
