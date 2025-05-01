using TMPro;
using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
