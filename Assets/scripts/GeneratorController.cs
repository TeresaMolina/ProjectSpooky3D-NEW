using UnityEngine;
using UnityEngine.Analytics;

public class GeneratorController : MonoBehaviour
{
    public Light lightToTurnOn;
    public AudioSource activationSound;
    public GeneratorManager manager;
    public GeneratorPromptUI promptUI; //drag the canvas thingy
    public ParticleSystem sparkEffect;

    private bool isActivated = false;
    private bool playerInRange = false;

    void Update()
    {
        if(playerInRange && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            ActivateGenerator();
        }
    }

    private void ActivateGenerator()
    {
        isActivated = true;

        if(lightToTurnOn != null)
        {
            lightToTurnOn.enabled = true;
        }
        if(activationSound != null)
        {
            activationSound.Play();
        }
        if (manager != null)
        {
            manager.GeneratorActivated();
        }
        if(promptUI != null)
        {
            promptUI.HidePrompt();
        }
        if (sparkEffect != null)
        {
            sparkEffect.Play();
        }

        Debug.Log("Gens Actiavted!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if(promptUI != null)
            {
                promptUI.ShowPrompt();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptUI != null)
            {
                promptUI.HidePrompt();
            }
        }


    }



}
